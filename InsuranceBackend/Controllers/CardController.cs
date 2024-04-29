using System.Data;
using InsuranceBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace InsuranceBackend.Controllers
{
    [Route("api/card")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IInsuranceContext _context;

        public CardController(IInsuranceContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] CardInfo card)
        {
            try
            {
                var cardOwnerParam = new SqlParameter("@card_owner", SqlDbType.NVarChar, 255) { Value = card.CardOwner };
                var cardNoParam = new SqlParameter("@card_no", SqlDbType.BigInt) { Value = card.CardNo };
                var securityCodeParam = new SqlParameter("@sec_code", SqlDbType.Int) { Value = card.SecurityCode };
                var validParam = new SqlParameter("@valid", SqlDbType.Date) { Value = card.ValidThrough };

                await _context.Database.ExecuteSqlRawAsync("EXEC ADD_CARD @card_owner, @card_no, @sec_code, @valid",
                    cardOwnerParam, cardNoParam, securityCodeParam, validParam);

                return Ok("Card added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred while adding card: {ex.Message}");
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateCard(int userId, [FromBody] CardInfo card)
        {
            try
            {
                var idParam = new SqlParameter("@card_id", SqlDbType.Int) { Value = userId };
                var cardOwnerParam = new SqlParameter("@card_owner", SqlDbType.NVarChar, 255) { Value = card.CardOwner };
                var cardNoParam = new SqlParameter("@card_no", SqlDbType.BigInt) { Value = card.CardNo };
                var securityCodeParam = new SqlParameter("@sec_code", SqlDbType.Int) { Value = card.SecurityCode };
                var validParam = new SqlParameter("@valid", SqlDbType.Date) { Value = card.ValidThrough };

                await _context.Database.ExecuteSqlRawAsync("EXEC UPDATE_CARD @card_id, @card_owner, @card_no, @sec_code, @valid",
                    idParam, cardOwnerParam, cardNoParam, securityCodeParam, validParam);

                return Ok("Card updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred while updating card: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IEnumerable<CardInfo>> getAllCards()
        {
            return await _context.CardInfos.ToListAsync();
        }
        [HttpGet("id/{id}",Name ="getCardById")]
        public async Task<IActionResult> getCardById(int id)
        {
            var card = await _context.CardInfos.FirstOrDefaultAsync(x => x.CardId == id);

            return Ok(card);
        }

        [HttpGet("name/{name}", Name = "getCardByName")]
        public async Task<IActionResult> GetCardByName(string name)
        {
            var card = await _context.CardInfos.FirstOrDefaultAsync(x => x.CardOwner == name);

            return Ok(card);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            try
            {
                var idParam = new SqlParameter("@card_id", SqlDbType.Int) { Value = id };

                await _context.Database.ExecuteSqlRawAsync("EXEC DELETE_CARD @card_id", idParam);

                return Ok("Card deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred while deleting card: {ex.Message}");
            }
        }
    }
}

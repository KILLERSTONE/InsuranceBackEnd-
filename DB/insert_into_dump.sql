--insert file adding dummy data
use iInsurance;
------------------------------------------FOR USER INFO-------------------------------------------------
DBCC CHECKIDENT (user_info,reseed, 0);
delete from login_log;
delete from user_info;
INSERT INTO user_info (email, username, password)
VALUES 
('admin@ilink.com', 'admin', 'admin123'),
('jeewa@ilink.com', 'jeewan', 'jeewan123'),
('dex@ilink.com', 'dex', 'ilink123');

select * from user_info;

-------------------------------------------FOR CARD INFO---------------------------------------------
INSERT INTO card_info (card_owner, card_no, security_code, valid_through)
VALUES 
('Raghav Purohit', 1234567890123456, 123, '2024-12-31'),
('Keshav Dibbelwala', 9876543210987654, 456, '2024-10-15'),
('Divya Napani', 4567890123456789, 789, '2025-06-30');

----------------------------------------------------FOR POLICIES----------------------------------------

INSERT INTO policies (user_id,policy_name, policy_detail, policy_insurer, policy_tpa, policy_from, policy_to)
VALUES 
(1,'Policy 1', 'Details for Policy 1', 'Insurer A', 'TPA 1', '2024-01-01', '2024-12-31'),
(2,'Policy 2', 'Details for Policy 2', 'Insurer B', NULL, '2023-06-15', '2025-06-15'),
(3,'Policy 3', 'Details for Policy 3', 'Insurer C', 'TPA 2', '2022-11-01', '2023-11-01');


select * from login_log;
select * from policies;
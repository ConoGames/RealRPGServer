
DROP TABLE Coupon;
DROP TABLE CouponInfo;
DROP TABLE MoneyItem;
DROP TABLE Receipt;
DROP TABLE MoneyItemInfo;
DROP TABLE Friend;
DROP TABLE FriendRequest;
DROP TABLE Achievement;
DROP TABLE AchievementInfo;
DROP TABLE DayOfDayAchievement;
DROP TABLE DayOfDayAchievementInfo;
DROP TABLE DayOfWeekAchievement;
DROP TABLE DayOfWeekAchievementInfo;
DROP TABLE DayOfMonthAchievement;
DROP TABLE DayOfMonthAchievementInfo;

DROP TABLE Mail;
DROP TABLE MailInfo;

DROP TABLE CharacterSkill;
DROP TABLE CharacterSkillInfo;
DROP TABLE CharacterJob;
DROP TABLE CharacterJobInfo;
DROP TABLE Characters;
DROP TABLE CharacterInfo;

DROP TABLE Stage;
DROP TABLE StageInfo;

DROP TABLE Equipment;
DROP TABLE EquipmentInfo;

DROP TABLE GuildUser;
DROP TABLE Guild;
DROP TABLE User;
DROP TABLE Account;


DROP DATABASE RealRpgDB;

CREATE DATABASE RealRpgDB;

USE RealRpgDB;


CREATE TABLE Account (
 accountNo BIGINT(1) AUTO_INCREMENT,
 loginToken VARCHAR(36) UNIQUE NOT NULL,
 
 registerTime TIMESTAMP NOT NULL,
 lastLoginTime TIMESTAMP NOT NULL,
 lastLogoutTime TIMESTAMP NOT NULL,

 PRIMARY KEY(accountNo),
 INDEX idx1(loginToken)
);


CREATE TABLE User (
 userNo BIGINT(1) NOT NULL,
 nickname VARCHAR(20) UNIQUE NOT NULL,
 exp INT NOT NULL DEFAULT 0,
 jam INT NOT NULL DEFAULT 0,
 gold INT NOT NULL DEFAULT 0,
 mileage INT NOT NULL DEFAULT 0,
 advTicket INT NOT NULL DEFAULT 0,
 pvpTicket INT NOT NULL DEFAULT 0,
 raidTicket INT NOT NULL DEFAULT 0,
 dayTicket INT NOT NULL DEFAULT 0,
 
 advTicketUseTime TIMESTAMP NOT NULL DEFAULT NOW(),
 pvpTicketUseTime TIMESTAMP NOT NULL DEFAULT NOW(),
 raidTicketUseTime TIMESTAMP NOT NULL DEFAULT NOW(),
 dayTicketUseTime TIMESTAMP NOT NULL DEFAULT NOW(),
 
 isJoinPvp CHAR(1) NOT NULL DEFAULT 'N',


 bestStrength INT NOT NULL DEFAULT 0,
 bestSynthesis INT NOT NULL DEFAULT 0,
 strengthCount INT NOT NULL DEFAULT 0,
 synthesisCount INT NOT NULL DEFAULT 0,
 sellCount INT NOT NULL DEFAULT 0,
 jewelPickCount INT NOT NULL DEFAULT 0,
 jamPickCount INT NOT NULL DEFAULT 0,
 adventureWinCount INT NOT NULL DEFAULT 0,
 pvpWinCount INT NOT NULL DEFAULT 0,
 tutorial INT NOT NULL DEFAULT 0,
 bestConsecuteWin INT NOT NULL DEFAULT 0,
 consecuteWin INT NOT NULL DEFAULT 0,
 -- currentStage VARCHAR(181) NOT NULL DEFAULT "0",
 
presentCount INT(1) NOT NULL DEFAULT 0,
presentTime TIMESTAMP NOT NULL DEFAULT NOW(),
 -- representCharType BIGINT(1) NOT NULL DEFAULT 0,
 -- level INT NOT NULL DEFAULT 1,
 -- exp INT NOT NULL DEFAULT 0,
 win INT NOT NULL DEFAULT 0,
 lose INT NOT NULL DEFAULT 0,
 point INT NOT NULL DEFAULT 0,

 FOREIGN KEY(userNo) REFERENCES Account(accountNo)
 ON DELETE CASCADE
 ON UPDATE CASCADE,

 INDEX idx1(userNo),
 INDEX idx2(nickname)
)DEFAULT CHARACTER SET utf8;


CREATE TABLE Guild (
 guildNo INT(1) AUTO_INCREMENT,
 name VARCHAR(20) NOT NULL,
 point INT NOT NULL DEFAULT 0,
 prevRank INT NOT NULL DEFAULT 0,
 win INT NOT NULL DEFAULT 0,
 lose INT NOT NULL DEFAULT 0,
 masterUserNo BIGINT(1) NOT NULL,

 PRIMARY KEY(guildNo),
 FOREIGN KEY(masterUserNo) REFERENCES User(userNo)
 ON DELETE CASCADE
 ON UPDATE CASCADE,
 INDEX idx1(masterUserNo),
 INDEX idx2(name)
);

CREATE TABLE GuildUser (
 guildNo INT(1) NOT NULL,
 userNo BIGINT(1) NOT NULL,
 class CHAR(1) NOT NULL DEFAULT 'F',

 PRIMARY KEY(guildNo, userNo),
 INDEX idx1(guildNo),
 INDEX idx2(userNo),
 FOREIGN KEY(userNo) REFERENCES User(userNo)
 ON DELETE CASCADE
 ON UPDATE CASCADE,
 FOREIGN KEY(guildNo) REFERENCES Guild(guildNo)
 ON DELETE CASCADE
 ON UPDATE CASCADE
);

CREATE TABLE EquipmentInfo (
 type INT NOT NULL,

 PRIMARY KEY(type)
);

CREATE TABLE Equipment (
 equipemntNo BIGINT(1) AUTO_INCREMENT,
 type INT NOT NULL,
 userNo BIGINT(1) NOT NULL,
 strength INT NOT NULL,
 strengthProb INT NOT NULL,

 PRIMARY KEY(equipemntNo),
 FOREIGN KEY(type) REFERENCES EquipmentInfo(type)
 ON DELETE CASCADE
 ON UPDATE CASCADE,
 FOREIGN KEY(userNo) REFERENCES User(userNo)
 ON DELETE CASCADE
 ON UPDATE CASCADE
);

CREATE TABLE StageInfo (
 type INT NOT NULL,

 PRIMARY KEY(type)
);

CREATE TABLE Stage (
 type INT NOT NULL,
 userNo BIGINT NOT NULL,
 starCount INT NOT NULL,
 lastClearTime TIMESTAMP NOT NULL,

 UNIQUE KEY(type, userNo),
 FOREIGN KEY(type) REFERENCES StageInfo(type)
 ON DELETE CASCADE
 ON UPDATE CASCADE,
 FOREIGN KEY(userNo) REFERENCES User(userNo)
 ON DELETE CASCADE
 ON UPDATE CASCADE
);

CREATE TABLE CharacterInfo (
 type INT UNIQUE NOT NULL,

 PRIMARY KEY(type)
);

CREATE TABLE Characters (
 type INT NOT NULL,
 affiliation INT NOT NULL,
 userNo BIGINT(1) NOT NULL,
 level INT(1) NOT NULL DEFAULT 1,
 exp INT(1) NOT NULL DEFAULT 0,

 helmetEquipmentNo BIGINT(1),
 bodyEquipmentNo BIGINT(1),
 bootsEquipmentNo BIGINT(1),
 weapon1EquipmentNo BIGINT(1),
 weapon2EquipmentNo BIGINT(1),
 necklaceEquipmentNo BIGINT(1),
 ringEquipmentNo BIGINT(1),
 accessoryEquipmentNo BIGINT(1),

 FOREIGN KEY(userNo) REFERENCES User(userNo)
 ON DELETE CASCADE
 ON UPDATE CASCADE,

 FOREIGN KEY(type) REFERENCES CharacterInfo(type)
 ON DELETE CASCADE
 ON UPDATE CASCADE
);

CREATE TABLE CharacterJobInfo (
 jobType INT UNIQUE NOT NULL,
 type INT NOT NULL,
 class CHAR(1) NOT NULL,

 PRIMARY KEY(jobType),
 FOREIGN KEY(type) REFERENCES CharacterInfo(type)
 ON DELETE CASCADE
 ON UPDATE CASCADE
);

CREATE TABLE CharacterJob (
 userNo BIGINT(1) NOT NULL,
 jobType INT NOT NULL,
 
 PRIMARY KEY(userNo, jobType),
 FOREIGN KEY(userNo) REFERENCES User(userNo)
 ON DELETE CASCADE
 ON UPDATE CASCADE,
 FOREIGN KEY(jobType) REFERENCES CharacterJobInfo(jobType)
 ON DELETE CASCADE
 ON UPDATE CASCADE
);

CREATE TABLE CharacterSkillInfo (
 skillType INT NOT NULL,
 jobType INT NOT NULL,
 maxLevel INT NOT NULL,

 PRIMARY KEY(skillType),
 FOREIGN KEY(jobType) REFERENCES CharacterJobInfo(jobType)
 ON DELETE CASCADE
 ON UPDATE CASCADE
);

CREATE TABLE CharacterSkill (
 skillType INT NOT NULL,
 userNo BIGINT(1) NOT NULL,
 level INT NOT NULL DEFAULT 0,

 FOREIGN KEY(skillType) REFERENCES CharacterSkillInfo(skillType)
 ON DELETE CASCADE
 ON UPDATE CASCADE,
 FOREIGN KEY(userNo) REFERENCES User(userNo)
 ON DELETE CASCADE
 ON UPDATE CASCADE
);


CREATE TABLE MailInfo (
 type BIGINT(1) AUTO_INCREMENT,
 title VARCHAR(50) NOT NULL,
 content VARCHAR(100) NOT NULL,
 rewardType INT(1) NOT NULL,
 rewardDetail INT(1) NOT NULL,
 PRIMARY KEY(type)
);

CREATE TABLE Mail (
 mailNo BIGINT(1) AUTO_INCREMENT,
 userNo BIGINT(1) NOT NULL,
 type BIGINT(1) NOT NULL,
 deleteTime TIMESTAMP NOT NULL,
 PRIMARY KEY(mailNo),
 FOREIGN KEY(userNo) REFERENCES User(userNo),
 FOREIGN KEY(type) REFERENCES MailInfo(type)
 ON DELETE CASCADE
 ON UPDATE CASCADE,
 INDEX idx1(userNo)
);


CREATE TABLE AchievementInfo (
 type INT(1) NOT NULL,
 maxClearCount INT(1) NOT NULL,
 maxCount INT(1) NOT NULL,
 PRIMARY KEY(type)
);


CREATE TABLE Achievement (
 userNo BIGINT(1) NOT NULL,
 type INT(1) NOT NULL,
 clearCount INT(1) NOT NULL,
 currentCount INT(1) NOT NULL DEFAULT 0,
 lastClearTime TIMESTAMP NOT NULL,
 lastUpdateTime TIMESTAMP NOT NULL,
 UNIQUE KEY(userNo, type),
 FOREIGN KEY(type) REFERENCES AchievementInfo(type),
 FOREIGN KEY(userNo) REFERENCES User(userNo),
 INDEX idx1(userNo)
);

CREATE TABLE DayOfDayAchievementInfo (
 type INT(1) NOT NULL,
 maxClearCount INT(1) NOT NULL,
 maxCount INT(1) NOT NULL,
 PRIMARY KEY(type)
);


CREATE TABLE DayOfDayAchievement (
 userNo BIGINT(1) NOT NULL,
 type INT(1) NOT NULL,
 clearCount INT(1) NOT NULL,
 currentCount INT(1) NOT NULL DEFAULT 0,
 lastClearTime TIMESTAMP NOT NULL,
 lastUpdateTime TIMESTAMP NOT NULL,
 UNIQUE KEY(userNo, type),
 FOREIGN KEY(type) REFERENCES DayOfDayAchievementInfo(type),
 FOREIGN KEY(userNo) REFERENCES User(userNo),
 INDEX idx1(userNo)
);

CREATE TABLE DayOfWeekAchievementInfo (
 type INT(1) NOT NULL,
 maxClearCount INT(1) NOT NULL,
 maxCount INT(1) NOT NULL,
 PRIMARY KEY(type)
);

CREATE TABLE DayOfWeekAchievement (
 userNo BIGINT(1) NOT NULL,
 type INT(1) NOT NULL,
 clearCount INT(1) NOT NULL,
 currentCount INT(1) NOT NULL DEFAULT 0,
 lastClearTime TIMESTAMP NOT NULL,
 lastUpdateTime TIMESTAMP NOT NULL,
 UNIQUE KEY(userNo, type),
 FOREIGN KEY(type) REFERENCES DayOfWeekAchievementInfo(type),
 FOREIGN KEY(userNo) REFERENCES User(userNo),
 INDEX idx1(userNo)
);


CREATE TABLE DayOfMonthAchievementInfo (
 type INT(1) NOT NULL,
 maxClearCount INT(1) NOT NULL,
 maxCount INT(1) NOT NULL,
 PRIMARY KEY(type)
);

CREATE TABLE DayOfMonthAchievement (
 userNo BIGINT(1) NOT NULL,
 type INT(1) NOT NULL,
 clearCount INT(1) NOT NULL,
 currentCount INT(1) NOT NULL DEFAULT 0,
 lastClearTime TIMESTAMP NOT NULL,
 lastUpdateTime TIMESTAMP NOT NULL,
 UNIQUE KEY(userNo, type),
 FOREIGN KEY(type) REFERENCES DayOfMonthAchievementInfo(type),
 FOREIGN KEY(userNo) REFERENCES User(userNo),
 INDEX idx1(userNo)
);

CREATE TABLE FriendRequest (
 requestUserNo BIGINT(1) NOT NULL,
 receiveUserNo BIGINT(1) NOT NULL,
 FOREIGN KEY(requestUserNo) REFERENCES User(userNo),
 FOREIGN KEY(receiveUserNo) REFERENCES User(userNo),
 INDEX idx1(requestUserNo),
 INDEX idx2(receiveUserNo)
);

CREATE TABLE Friend (
 userNo1 BIGINT(1) NOT NULL,
 userNo2 BIGINT(1) NOT NULL,
 PRIMARY KEY(userNo1, userNo2),
 FOREIGN KEY(userNo1) REFERENCES User(userNo),
 FOREIGN KEY(userNo2) REFERENCES User(userNo),
 INDEX idx1(userNo1)
);




CREATE TABLE MoneyItemInfo (
 type INT NOT NULL,
 price INT NOT NULL,
 PRIMARY KEY(type)
);

CREATE TABLE MoneyItem (
 userNo BIGINT(1) NOT NULL,
 type INT NOT NULL,
 purchaseTime TIMESTAMP NOT NULL DEFAULT NOW(),
 PRIMARY KEY(userNo, type),
 FOREIGN KEY(userNo) REFERENCES User(userNo),
 FOREIGN KEY(type) REFERENCES MoneyItemInfo(type)
);

CREATE TABLE Receipt (
 receiptNo BIGINT(1) AUTO_INCREMENT,
 userNo BIGINT(1) NOT NULL,
 itemType INT NOT NULL,
 publicToken VARCHAR(500) NOT NULL,
 registerTime TIMESTAMP NOT NULL DEFAULT NOW(),
 PRIMARY KEY(receiptNo),
 FOREIGN KEY(userNo) REFERENCES User(userNo),
 FOREIGN KEY(itemType) REFERENCES MoneyItemInfo(type)

);

CREATE TABLE CouponInfo (
 type INT(1) NOT NULL AUTO_INCREMENT,
 couponNumber VARCHAR(36) NOT NULL,
 expiration INT NOT NULL DEFAULT 1,
 
 PRIMARY KEY(type),

 UNIQUE KEY(couponNumber),
 INDEX idx1(couponNumber)
);

CREATE TABLE Coupon (
 type INT(1) NOT NULL,
 userNo BIGINT(1) NOT NULL,
 useTime TIMESTAMP NOT NULL,

 UNIQUE KEY(type, userNo),
 INDEX idx1(userNo),
 FOREIGN KEY(userNo) REFERENCES User(userNo),
 FOREIGN KEY(type) REFERENCES CouponInfo(type)
);







































-- INSERT INTO CardType(cardType) VALUES (120);

-- ALTER TABLE User ADD isGetSpecialCard120 INT NOT NULL DEFAULT 0;


-- ALTER TABLE User ADD physicsMeteoriteMaterial INT NOT NULL DEFAULT 0;
-- ALTER TABLE User ADD spellMeteoriteMaterial INT NOT NULL DEFAULT 0;
-- ALTER TABLE User ADD holyMeteoriteMaterial INT NOT NULL DEFAULT 0;
-- ALTER TABLE User ADD darkMeteoriteMaterial INT NOT NULL DEFAULT 0;
-- ALTER TABLE User ADD specialMeteoriteMaterial INT NOT NULL DEFAULT 0;

-- ALTER TABLE User ADD bestPhysicsStrength INT NOT NULL DEFAULT 0;
-- ALTER TABLE User ADD bestSpellStrength INT NOT NULL DEFAULT 0;
-- ALTER TABLE User ADD bestHolyStrength INT NOT NULL DEFAULT 0;
-- ALTER TABLE User ADD bestDarkStrength INT NOT NULL DEFAULT 0;
-- ALTER TABLE User ADD bestSpecialStrength INT NOT NULL DEFAULT 0;

-- ALTER TABLE User ADD physicsStrengthCount INT NOT NULL DEFAULT 0;
-- ALTER TABLE User ADD spellStrengthCount INT NOT NULL DEFAULT 0;
-- ALTER TABLE User ADD holyStrengthCount INT NOT NULL DEFAULT 0;
-- ALTER TABLE User ADD darkStrengthCount INT NOT NULL DEFAULT 0;
-- ALTER TABLE User ADD specialStrengthCount INT NOT NULL DEFAULT 0;

-- ALTER TABLE User ADD bossCoin INT NOT NULL DEFAULT 0;
 
-- ALTER TABLE User ADD useBossCoinTime TIMESTAMP NOT NULL DEFAULT NOW();



-- CREATE TABLE Meteorite (
--  meteoriteNo BIGINT(1) NOT NULL,
--  userNo BIGINT(1) NOT NULL,
--  cardNo BIGINT(1) DEFAULT 0,
--  index INT DEFAULT 0,
--  meteoriteType INT NOT NULL,
--  strength INT NOT NULL DEFAULT 0,
--  userNo BIGINT(1) NOT NULL,
--  PRIMARY KEY(meteoriteNo),
--  FOREIGN KEY(userNo) REFERENCES User(userNo),
--  FOREIGN KEY(cardNo) REFERENCES Card(cardNo),
--  INDEX idx1(userNo),
--  UNIQUE KEY(cardNo, index)
-- );

-- INSERT INTO CardType(cardType) VALUES (0);
-- INSERT INTO CardType(cardType) VALUES (1);
-- INSERT INTO CardType(cardType) VALUES (2);
-- INSERT INTO CardType(cardType) VALUES (3);
-- INSERT INTO CardType(cardType) VALUES (4);
-- INSERT INTO CardType(cardType) VALUES (5);
-- INSERT INTO CardType(cardType) VALUES (6);
-- INSERT INTO CardType(cardType) VALUES (7);
-- INSERT INTO CardType(cardType) VALUES (8);
-- INSERT INTO CardType(cardType) VALUES (9);
-- INSERT INTO CardType(cardType) VALUES (10);
-- INSERT INTO CardType(cardType) VALUES (11);
-- INSERT INTO CardType(cardType) VALUES (12);
-- INSERT INTO CardType(cardType) VALUES (13);
-- INSERT INTO CardType(cardType) VALUES (14);
-- INSERT INTO CardType(cardType) VALUES (15);
-- INSERT INTO CardType(cardType) VALUES (16);
-- INSERT INTO CardType(cardType) VALUES (17);
-- INSERT INTO CardType(cardType) VALUES (18);
-- INSERT INTO CardType(cardType) VALUES (19);
-- INSERT INTO CardType(cardType) VALUES (20);
-- INSERT INTO CardType(cardType) VALUES (21);
-- INSERT INTO CardType(cardType) VALUES (22);
-- INSERT INTO CardType(cardType) VALUES (23);
-- INSERT INTO CardType(cardType) VALUES (24);
-- INSERT INTO CardType(cardType) VALUES (25);
-- INSERT INTO CardType(cardType) VALUES (26);
-- INSERT INTO CardType(cardType) VALUES (27);
-- INSERT INTO CardType(cardType) VALUES (28);
-- INSERT INTO CardType(cardType) VALUES (29);
-- INSERT INTO CardType(cardType) VALUES (30);
-- INSERT INTO CardType(cardType) VALUES (31);
-- INSERT INTO CardType(cardType) VALUES (32);
-- INSERT INTO CardType(cardType) VALUES (33);
-- INSERT INTO CardType(cardType) VALUES (34);
-- INSERT INTO CardType(cardType) VALUES (35);
-- INSERT INTO CardType(cardType) VALUES (36);
-- INSERT INTO CardType(cardType) VALUES (37);
-- INSERT INTO CardType(cardType) VALUES (38);
-- INSERT INTO CardType(cardType) VALUES (39);
-- INSERT INTO CardType(cardType) VALUES (40);
-- INSERT INTO CardType(cardType) VALUES (41);
-- INSERT INTO CardType(cardType) VALUES (42);
-- INSERT INTO CardType(cardType) VALUES (43);
-- INSERT INTO CardType(cardType) VALUES (44);
-- INSERT INTO CardType(cardType) VALUES (45);
-- INSERT INTO CardType(cardType) VALUES (46);
-- INSERT INTO CardType(cardType) VALUES (47);
-- INSERT INTO CardType(cardType) VALUES (48);
-- INSERT INTO CardType(cardType) VALUES (49);
-- INSERT INTO CardType(cardType) VALUES (50);
-- INSERT INTO CardType(cardType) VALUES (51);
-- INSERT INTO CardType(cardType) VALUES (52);
-- INSERT INTO CardType(cardType) VALUES (53);
-- INSERT INTO CardType(cardType) VALUES (54);
-- INSERT INTO CardType(cardType) VALUES (55);
-- INSERT INTO CardType(cardType) VALUES (56);
-- INSERT INTO CardType(cardType) VALUES (57);
-- INSERT INTO CardType(cardType) VALUES (58);
-- INSERT INTO CardType(cardType) VALUES (59);
-- INSERT INTO CardType(cardType) VALUES (60);
-- INSERT INTO CardType(cardType) VALUES (61);
-- INSERT INTO CardType(cardType) VALUES (62);
-- INSERT INTO CardType(cardType) VALUES (63);
-- INSERT INTO CardType(cardType) VALUES (64);
-- INSERT INTO CardType(cardType) VALUES (65);
-- INSERT INTO CardType(cardType) VALUES (66);
-- INSERT INTO CardType(cardType) VALUES (67);
-- INSERT INTO CardType(cardType) VALUES (68);
-- INSERT INTO CardType(cardType) VALUES (69);
-- INSERT INTO CardType(cardType) VALUES (70);
-- INSERT INTO CardType(cardType) VALUES (71);
-- INSERT INTO CardType(cardType) VALUES (72);
-- INSERT INTO CardType(cardType) VALUES (73);
-- INSERT INTO CardType(cardType) VALUES (74);
-- INSERT INTO CardType(cardType) VALUES (75);
-- INSERT INTO CardType(cardType) VALUES (76);
-- INSERT INTO CardType(cardType) VALUES (77);
-- INSERT INTO CardType(cardType) VALUES (78);
-- INSERT INTO CardType(cardType) VALUES (79);
-- INSERT INTO CardType(cardType) VALUES (80);
-- INSERT INTO CardType(cardType) VALUES (81);
-- INSERT INTO CardType(cardType) VALUES (82);
-- INSERT INTO CardType(cardType) VALUES (83);
-- INSERT INTO CardType(cardType) VALUES (84);
-- INSERT INTO CardType(cardType) VALUES (85);
-- INSERT INTO CardType(cardType) VALUES (86);
-- INSERT INTO CardType(cardType) VALUES (87);
-- INSERT INTO CardType(cardType) VALUES (88);
-- INSERT INTO CardType(cardType) VALUES (89);
-- INSERT INTO CardType(cardType) VALUES (90);
-- INSERT INTO CardType(cardType) VALUES (91);
-- INSERT INTO CardType(cardType) VALUES (92);
-- INSERT INTO CardType(cardType) VALUES (93);
-- INSERT INTO CardType(cardType) VALUES (94);
-- INSERT INTO CardType(cardType) VALUES (95);
-- INSERT INTO CardType(cardType) VALUES (96);
-- INSERT INTO CardType(cardType) VALUES (97);
-- INSERT INTO CardType(cardType) VALUES (98);
-- INSERT INTO CardType(cardType) VALUES (99);
-- INSERT INTO CardType(cardType) VALUES (100);
-- INSERT INTO CardType(cardType) VALUES (101);
-- INSERT INTO CardType(cardType) VALUES (102);
-- INSERT INTO CardType(cardType) VALUES (103);
-- INSERT INTO CardType(cardType) VALUES (104);
-- INSERT INTO CardType(cardType) VALUES (105);
-- INSERT INTO CardType(cardType) VALUES (106);
-- INSERT INTO CardType(cardType) VALUES (107);
-- INSERT INTO CardType(cardType) VALUES (108);
-- INSERT INTO CardType(cardType) VALUES (109);
-- INSERT INTO CardType(cardType) VALUES (110);
-- INSERT INTO CardType(cardType) VALUES (111);
-- INSERT INTO CardType(cardType) VALUES (112);
-- INSERT INTO CardType(cardType) VALUES (113);
-- INSERT INTO CardType(cardType) VALUES (114);
-- INSERT INTO CardType(cardType) VALUES (115);
-- INSERT INTO CardType(cardType) VALUES (116);
-- INSERT INTO CardType(cardType) VALUES (117);
-- INSERT INTO CardType(cardType) VALUES (118);
-- INSERT INTO CardType(cardType) VALUES (119);



-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (0, 1);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (1, 36);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (2, 0);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (3, 18);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (4, 6);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (5, 7);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (6, 4);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (7, 0);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (8, 0);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (9, 0);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (10, 0);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (11, 0);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (12, 0);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (13, 10);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (14, 4);
-- INSERT INTO AchievementType(achievementTypeNo, maxLevel) VALUES (15, 4);



-- UPDATE User SET lastLoginTime = "2015-01-01 00:00:00";
-- INSERT INTO FriendRequest(requestUserNo, receiveUserNo) VALUES (1, 2);
-- INSERT INTO Friend(userNo1, userNo2) VALUES (1, 2);
-- INSERT INTO Friend(userNo1, userNo2) VALUES (2, 1);

-- INSERT INTO Mail(mailTypeNo, userNo, deleteTime) VALUES (1, 1, "2017-06-22 05:00:00");

-- SELECT f.userNo2, u.nickname, u.level FROM Friend as f, User as u WHERE f.userNo2 = 2 and f.userNo2 = u.userNo;


-- INSERT INTO Friend(userNo1, userNo2) VALUES (1, 2);

-- SELECT u.*, t.* FROM Team as t, User as u WHERE u.userNo = 2 and t.userNo = u.userNo and t.teamIndex = u.teamPosition;


-- SELECT f.userNo2, u.nickname, u.level, u.representCardType FROM Friend AS f, User AS u WHERE f.userNo2 = 1 and f.userNo2 = u.userNo;



-- SELECT u.*, t.cardNo1, t.cardNo2, t.cardNo3, t.cardNo4 FROM Team as t, User as u WHERE u.nickname = "test100" and t.userNo = u.userNo and t.teamIndex = u.teamPosition;

-- SELECT m.mailNo, m.mailTypeNo, mt.title, mt.content, mt.rewardType, mt.rewardCount, m.deleteTime FROM Mail AS m, MailType AS mt WHERE m.mailTypeNo = mt.mailTypeNo AND userNo = 1;

-- INSERT INTO OneTimeItem(userNo, itemType, level) VALUES (13758, 1, 1);




INSERT INTO Account(loginToken, lastLoginTime, lastLogoutTime, registerTime) VALUES ("1234", "2017-06-22 05:00:00", "2017-06-22 05:00:00", "2017-06-22 05:00:00");




/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : melia

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-24 14:22:44
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for characters
-- ----------------------------
DROP TABLE IF EXISTS `characters`;
CREATE TABLE `characters` (
  `characterId` bigint(20) NOT NULL AUTO_INCREMENT,
  `accountId` bigint(20) NOT NULL,
  `name` varchar(64) NOT NULL,
  `teamName` varchar(32) DEFAULT NULL,
  `job` smallint(6) NOT NULL,
  `gender` tinyint(4) NOT NULL,
  `hair` tinyint(4) NOT NULL,
  `level` int(11) NOT NULL DEFAULT '1',
  `barrackLayer` int(11) NOT NULL DEFAULT '1',
  `bx` float NOT NULL,
  `by` float NOT NULL,
  `bz` float NOT NULL,
  `zone` int(11) NOT NULL,
  `x` float NOT NULL,
  `y` float NOT NULL,
  `z` float NOT NULL,
  `exp` int(11) NOT NULL DEFAULT '0',
  `maxExp` int(11) NOT NULL DEFAULT '1000',
  `totalExp` int(11) NOT NULL DEFAULT '0',
  `hp` int(11) NOT NULL DEFAULT '100',
  `maxHp` int(11) NOT NULL DEFAULT '100',
  `sp` int(11) NOT NULL DEFAULT '50',
  `maxSp` int(11) NOT NULL DEFAULT '50',
  `stamina` int(11) NOT NULL DEFAULT '25000',
  `maxStamina` int(11) NOT NULL DEFAULT '25000',
  `str` int(11) NOT NULL DEFAULT '1',
  `con` int(11) NOT NULL DEFAULT '1',
  `int` int(11) NOT NULL DEFAULT '1',
  `spr` int(11) NOT NULL DEFAULT '1',
  `dex` int(11) NOT NULL DEFAULT '1',
  `statByLevel` int(11) NOT NULL DEFAULT '0',
  `statByBonus` int(11) NOT NULL DEFAULT '0',
  `usedStat` int(11) NOT NULL DEFAULT '0',
  `abilityPoints` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`characterId`),
  KEY `accountId` (`accountId`),
  CONSTRAINT `characters_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `accounts` (`accountid`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of characters
-- ----------------------------
SET FOREIGN_KEY_CHECKS=1;

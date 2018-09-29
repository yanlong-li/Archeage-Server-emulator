/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : melia

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-24 13:54:06
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for character_abilities
-- ----------------------------
DROP TABLE IF EXISTS `character_abilities`;
CREATE TABLE `character_abilities` (
  `abilityId` bigint(20) NOT NULL AUTO_INCREMENT,
  `characterId` bigint(20) NOT NULL,
  `id` int(11) NOT NULL,
  `level` int(11) NOT NULL,
  PRIMARY KEY (`abilityId`),
  KEY `characterId` (`characterId`),
  CONSTRAINT `abilities_ibfk_1` FOREIGN KEY (`characterId`) REFERENCES `character_data` (`characterid`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of character_abilities
-- ----------------------------
SET FOREIGN_KEY_CHECKS=1;

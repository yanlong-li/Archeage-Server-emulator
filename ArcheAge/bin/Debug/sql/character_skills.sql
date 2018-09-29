/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : melia

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-24 13:54:44
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for skills
-- ----------------------------
DROP TABLE IF EXISTS `skills`;
CREATE TABLE `skills` (
  `skillId` bigint(20) NOT NULL AUTO_INCREMENT,
  `characterId` bigint(20) NOT NULL,
  `id` int(11) NOT NULL,
  `level` int(11) NOT NULL,
  PRIMARY KEY (`skillId`),
  KEY `characterId` (`characterId`),
  CONSTRAINT `skills_ibfk_1` FOREIGN KEY (`characterId`) REFERENCES `characters` (`characterid`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of skills
-- ----------------------------
SET FOREIGN_KEY_CHECKS=1;

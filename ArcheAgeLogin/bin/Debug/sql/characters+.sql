/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-07-26 01:33:56
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for characters
-- ----------------------------
DROP TABLE IF EXISTS `characters`;
CREATE TABLE `characters` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'DI',
  `AccountID` bigint(20) NOT NULL COMMENT 'Account ID',
  `WorldID` tinyint(2) NOT NULL,
  `Type` int(20) NOT NULL,
  `CharName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'CharName',
  `CharRace` tinyint(2) NOT NULL COMMENT 'CharRace',
  `CharGender` tinyint(2) NOT NULL COMMENT 'CharGender',
  `GUID` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'параметры чара',
  `V` double(20,0) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of characters
-- ----------------------------
INSERT INTO `characters` VALUES ('1', '50970', '1', '103639', 'Remota', '3', '2', 'DC0D0CFCD3E01847AD2A5D55EA471CDF', '0');
INSERT INTO `characters` VALUES ('2', '50970', '1', '124822', 'Develo', '1', '2', 'CECB8598F541B04BA674C7834DDC5D14', '0');
INSERT INTO `characters` VALUES ('3', '2', '1', '103639', 'Remotatwo', '3', '2', 'DC0D0CFCD3E01847AD2A5D55EA471CDF', '0');
SET FOREIGN_KEY_CHECKS=1;

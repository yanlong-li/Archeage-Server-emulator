/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-09 01:13:45
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for characters
-- ----------------------------
DROP TABLE IF EXISTS `characters`;
CREATE TABLE `characters` (
  `id` bigint(11) NOT NULL AUTO_INCREMENT,
  `accountid` int(11) DEFAULT NULL,
  `characterid` int(11) DEFAULT NULL,
  `chargender` tinyint(3) unsigned DEFAULT NULL,
  `charname` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `charrace` tinyint(3) unsigned DEFAULT NULL,
  `decor` int(11) DEFAULT NULL,
  `ext` tinyint(3) DEFAULT NULL,
  `eyebrow` int(11) DEFAULT NULL,
  `guid` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `leftpupil` int(11) DEFAULT NULL,
  `level` tinyint(4) DEFAULT NULL,
  `lip` int(11) DEFAULT NULL,
  `modifiers` varchar(512) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
  `movex` smallint(5) DEFAULT NULL,
  `movey` smallint(5) DEFAULT NULL,
  `rightpupil` int(255) DEFAULT NULL,
  `rotate` decimal(9,2) DEFAULT NULL,
  `scale` decimal(9,2) DEFAULT NULL,
  `type0` int(255) DEFAULT NULL,
  `type1` int(255) DEFAULT NULL,
  `type2` int(255) DEFAULT NULL,
  `type3` int(255) DEFAULT NULL,
  `type4` int(255) DEFAULT NULL,
  `type5` int(255) DEFAULT NULL,
  `type6` int(255) DEFAULT NULL,
  `type7` int(255) DEFAULT NULL,
  `type8` int(255) DEFAULT NULL,
  `type9` int(255) DEFAULT NULL,
  `type10` int(255) DEFAULT NULL,
  `type11` int(255) DEFAULT NULL,
  `type12` int(255) DEFAULT NULL,
  `type13` int(255) DEFAULT NULL,
  `type14` int(255) DEFAULT NULL,
  `type15` int(255) DEFAULT NULL,
  `type16` int(255) DEFAULT NULL,
  `type17` int(255) DEFAULT NULL,
  `v` bigint(255) DEFAULT NULL,
  `weight0` decimal(9,2) DEFAULT NULL,
  `weight1` decimal(9,2) DEFAULT NULL,
  `weight2` decimal(9,2) DEFAULT NULL,
  `weight3` decimal(9,2) DEFAULT NULL,
  `weight4` decimal(9,2) DEFAULT NULL,
  `weight5` decimal(9,2) DEFAULT NULL,
  `weight6` decimal(9,2) DEFAULT NULL,
  `weight7` decimal(9,2) DEFAULT NULL,
  `weight8` decimal(9,2) DEFAULT NULL,
  `weight9` decimal(9,2) DEFAULT NULL,
  `weight10` decimal(9,2) DEFAULT NULL,
  `weight11` decimal(9,2) DEFAULT NULL,
  `weight12` decimal(9,2) DEFAULT NULL,
  `weight13` decimal(9,2) DEFAULT NULL,
  `weight14` decimal(9,2) DEFAULT NULL,
  `weight15` decimal(9,2) DEFAULT NULL,
  `weight16` decimal(9,2) DEFAULT NULL,
  `weight17` decimal(9,2) DEFAULT NULL,
  `worldId` tinyint(3) unsigned DEFAULT NULL,
  `ability0` tinyint(3) unsigned DEFAULT NULL,
  `ability1` tinyint(3) unsigned DEFAULT NULL,
  `ability2` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of characters
-- ----------------------------
INSERT INTO `characters` VALUES ('16', '50970', '1706495', '2', 'justtocheck', '1', '0', '3', '-13088680', 'DC0D0CFCD3E01847AD2A5D55EA471CDF', '-1053521', '1', '-7635997', '00EF00EF00EE000103000000000000110000000000FE00063BB900D800EE00D400281BEBE100E700F037230000000000640000000000000064000000F0000000000000002BD50000006400000000F9000000E0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000\0\0', '0', '0', '-1053521', '0.00', '1.00', '1706495', '25372', '0', '0', '0', '0', '0', '4299', '4', '0', '0', '0', '444', '170', '0', '0', '0', '0', '0', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '0.00', '1.00', '1.00', '1.00', '1.00', '1.00', '0.00', '0.00', '1.00', '1', '1', '11', '11');
SET FOREIGN_KEY_CHECKS=1;

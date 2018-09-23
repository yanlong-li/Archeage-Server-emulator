/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-23 02:23:52
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for character_records
-- ----------------------------
DROP TABLE IF EXISTS `character_records`;
CREATE TABLE `character_records` (
  `characterid` int(11) unsigned NOT NULL,
  `accountid` int(11) unsigned DEFAULT NULL,
  `chargender` tinyint(3) unsigned DEFAULT NULL,
  `charname` varchar(80) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT '',
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
  `rightpupil` int(11) DEFAULT NULL,
  `rotate` double DEFAULT NULL,
  `scale` double DEFAULT NULL,
  `type0` int(11) DEFAULT NULL,
  `type1` int(11) DEFAULT NULL,
  `type2` int(11) DEFAULT NULL,
  `type3` int(11) DEFAULT NULL,
  `type4` int(11) DEFAULT NULL,
  `type5` int(11) DEFAULT NULL,
  `type6` int(11) DEFAULT NULL,
  `type7` int(11) DEFAULT NULL,
  `type8` int(11) DEFAULT NULL,
  `type9` int(11) DEFAULT NULL,
  `type10` int(11) DEFAULT NULL,
  `type11` int(11) DEFAULT NULL,
  `type12` int(11) DEFAULT NULL,
  `type13` int(11) DEFAULT NULL,
  `type14` int(11) DEFAULT NULL,
  `type15` int(11) DEFAULT NULL,
  `type16` int(11) DEFAULT NULL,
  `type17` int(11) DEFAULT NULL,
  `v` bigint(20) DEFAULT NULL,
  `weight0` double DEFAULT NULL,
  `weight1` double DEFAULT NULL,
  `weight2` double DEFAULT NULL,
  `weight3` double DEFAULT NULL,
  `weight4` double DEFAULT NULL,
  `weight5` double DEFAULT NULL,
  `weight6` double DEFAULT NULL,
  `weight7` double DEFAULT NULL,
  `weight8` double DEFAULT NULL,
  `weight9` double DEFAULT NULL,
  `weight10` double DEFAULT NULL,
  `weight11` double DEFAULT NULL,
  `weight12` double DEFAULT NULL,
  `weight13` double DEFAULT NULL,
  `weight14` double DEFAULT NULL,
  `weight15` double DEFAULT NULL,
  `weight16` double DEFAULT NULL,
  `weight17` double DEFAULT NULL,
  `worldId` tinyint(3) unsigned DEFAULT NULL,
  `ability0` tinyint(3) unsigned DEFAULT NULL,
  `ability1` tinyint(3) unsigned DEFAULT NULL,
  `ability2` tinyint(3) unsigned DEFAULT NULL,
  `liveobjectid` int(11) DEFAULT NULL,
  PRIMARY KEY (`characterid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of character_records
-- ----------------------------
INSERT INTO `character_records` VALUES ('1', '50970', '1', 'newbie', '1', '-12042656', '3', '-12227665', 'DC0D0CFCD3E01847AD2A5D55EA471CDF', '-12356390', '1', '-4279809', '00D3000011DCF00F1D0500002000000E0000202500FAFE093DE4D70000BC00380000131D0000001FFF00150000001F0001000000D3000800D700000027000FEF01260000260C000044291500000000002DCA1D000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000\0\0', '0', '0', '-12356390', '0', '1', '19838', '24127', '0', '0', '0', '0', '0', '1537', '1', '0', '0', '316', '0', '563', '683', '0', '31', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '1', '1', '1', '1', '0', '0', '0', '1', '1', '11', '11', '1');
SET FOREIGN_KEY_CHECKS=1;

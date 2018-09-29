/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-29 02:27:58
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `accountid` int(10) unsigned NOT NULL COMMENT 'di',
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT 'name',
  `token` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `mainaccess` tinyint(3) unsigned NOT NULL COMMENT 'Account level? GM? MEMBER? ',
  `useraccess` tinyint(3) unsigned NOT NULL COMMENT 'User id',
  `last_ip` varchar(20) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT 'Last login IP',
  `last_online` bigint(20) DEFAULT NULL COMMENT 'Last login time',
  `characters` tinyint(3) unsigned DEFAULT '0' COMMENT 'character? feature? quality? What a ghost! \r\n Probably not an administrator or VIP',
  `cookie` int(11) DEFAULT NULL,
  PRIMARY KEY (`accountid`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Account table';

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES ('50970', 'aatest', '31e34f2b72d93bb25d5f27be8a94c478', '1', '1', '127.0.0.1', '1538176508908', '2', '176321919');
SET FOREIGN_KEY_CHECKS=1;

/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-07-26 01:33:45
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'di',
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT 'name',
  `password` varchar(200) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `token` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `mainaccess` tinyint(1) NOT NULL COMMENT 'Account level? GM? MEMBER? ',
  `useraccess` tinyint(1) NOT NULL COMMENT 'User id',
  `last_ip` varchar(10) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT 'Last login IP',
  `last_online` bigint(20) DEFAULT NULL COMMENT 'Last login time',
  `characters` tinyint(20) DEFAULT '0' COMMENT 'character? feature? quality? What a ghost! \r\n Probably not an administrator or VIP',
  `cookie` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=50971 DEFAULT CHARSET=latin1 COMMENT='Account table\r\naccountTable\r\nusers table';

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES ('2', 'bbtest', 'password', '5f4dcc3b5aa765d61d8327deb882cf99', '1', '1', '127.0.0.1', '8388607', '1', '12345');
INSERT INTO `accounts` VALUES ('3', 'cctest', 'ttestt', 'b57dab52b3136872d303324fdd61faab', '1', '1', '127.0.0.1', '1532456040944', '0', '-1316359011');
INSERT INTO `accounts` VALUES ('50970', 'aatest', 'aatestaa', '31e34f2b72d93bb25d5f27be8a94c478', '1', '1', '127.0.0.1', '1532557959893', '2', '2058620968');
SET FOREIGN_KEY_CHECKS=1;

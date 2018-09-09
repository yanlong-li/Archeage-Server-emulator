/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-07 19:44:43
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'di',
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL COMMENT 'name',
  `token` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin NOT NULL,
  `mainaccess` tinyint(1) unsigned NOT NULL COMMENT 'Account level? GM? MEMBER? ',
  `useraccess` tinyint(1) unsigned NOT NULL COMMENT 'User id',
  `last_ip` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL COMMENT 'Last login IP',
  `last_online` bigint(20) DEFAULT NULL COMMENT 'Last login time',
  `characters` tinyint(1) unsigned DEFAULT '0' COMMENT 'character? feature? quality? What a ghost! \r\n Probably not an administrator or VIP',
  `cookie` varchar(255) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=50971 DEFAULT CHARSET=utf8 COMMENT='Account table';

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES ('2', 'bbtest', '5f4dcc3b5aa765d61d8327deb882cf99', '1', '1', '127.0.0.1', '1536020008763', '1', '168671328');
INSERT INTO `accounts` VALUES ('3', 'cctest', 'b57dab52b3136872d303324fdd61faab', '1', '1', '127.0.0.1', '1534518899567', '0', '-1442424842');
INSERT INTO `accounts` VALUES ('50970', 'aatest', '31e34f2b72d93bb25d5f27be8a94c478', '1', '1', '127.0.0.1', '1536329031008', '2', '-1117042693');
SET FOREIGN_KEY_CHECKS=1;

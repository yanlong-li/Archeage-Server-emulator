/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 50140
Source Host           : localhost:3306
Source Database       : archeage

Target Server Type    : MYSQL
Target Server Version : 50140
File Encoding         : 65001

Date: 2018-04-20 18:25:24
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `id` int(20) NOT NULL AUTO_INCREMENT COMMENT 'di',
  `accountid` int(20) DEFAULT NULL,
  `name` varchar(255) NOT NULL COMMENT 'name',
  `password` varchar(200) NOT NULL,
  `token` varchar(255) NOT NULL,
  `mainaccess` tinyint(1) NOT NULL COMMENT 'Account level? GM? MEMBER? ',
  `useraccess` tinyint(1) NOT NULL COMMENT 'User id',
  `last_ip` varchar(10) DEFAULT NULL COMMENT 'Last login IP',
  `last_online` bigint(20) DEFAULT NULL COMMENT 'Last login time',
  `characters` int(20) DEFAULT '0' COMMENT 'character? feature? quality? What a ghost! \r\n Probably not an administrator or VIP',
  `cookie` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COMMENT='Account table\r\naccountTable\r\nusers table';

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES ('1', '1', 'aatest', '31e34f2b72d93bb25d5f27be8a94c478', '31e34f2b72d93bb25d5f27be8a94c478', '1', '1', '127.0.0.1', '1523981422345', '0', '128665876');
INSERT INTO `accounts` VALUES ('2', '2', 'test', 'e10adc3949ba59abbe56e057f20f883e', 'e10adc3949ba59abbe56e057f20f883e', '1', '1', '127.0.0.1', '8388607', '0', '12345');
SET FOREIGN_KEY_CHECKS=1;

/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-07-02 01:37:27
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT 'di',
  `accountid` bigint(20) NOT NULL COMMENT 'Acc ID',
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1 COMMENT='Account table\r\naccountTable\r\nusers table';

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES ('1', '1', 'aatest', 'aatestaa', '31e34f2b72d93bb25d5f27be8a94c478', '1', '1', '127.0.0.1', '1530484544151', '2', '2091488167');
INSERT INTO `accounts` VALUES ('2', '2', 'test', 'password', '5f4dcc3b5aa765d61d8327deb882cf99', '1', '1', '127.0.0.1', '8388607', '0', '12345');
INSERT INTO `accounts` VALUES ('3', '3', 'ttest', 'ttestt', 'b57dab52b3136872d303324fdd61faab', '1', '1', '127.0.0.1', '8388607', '0', '0');
SET FOREIGN_KEY_CHECKS=1;

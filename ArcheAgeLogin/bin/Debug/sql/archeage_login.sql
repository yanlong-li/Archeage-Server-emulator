/*
 Navicat Premium Data Transfer

 Source Server         : 127.0.0.1
 Source Server Type    : MySQL
 Source Server Version : 100109
 Source Host           : 127.0.0.1:3306
 Source Schema         : archeage

 Target Server Type    : MySQL
 Target Server Version : 50799
 File Encoding         : 65001

 Date: 21/11/2017 22:30:19
*/

SET NAMES utf8;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts`  (
  `id` int(20) NOT NULL AUTO_INCREMENT COMMENT 'di',
  `name` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL COMMENT 'name',
  `mainaccess` tinyint(1) NOT NULL COMMENT '账户等级？GM？MEMBER？',
  `useraccess` tinyint(1) NOT NULL COMMENT '用户id',
  `password` varchar(200) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL COMMENT '密码',
  `token` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL COMMENT '账户登陆一次性token',
  `last_ip` varchar(10) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL COMMENT '最后登录IP',
  `last_online` mediumint(20) NULL DEFAULT NULL COMMENT '最后登陆时间',
  `characters` int(20) NULL DEFAULT 0 COMMENT '人物？特征？品质？什么鬼！\r\n大概是不是管理员或者VIP',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci COMMENT = '账户表\r\naccountTable\r\nusers table';

-- ----------------------------
-- Records of accounts
-- ----------------------------
BEGIN;
INSERT INTO `accounts` VALUES (1, 'test', 1, 1, 'e10adc3949ba59abbe56e057f20f883e', 'e10adc3949ba59abbe56e057f20f883e', '127.0.0.1', 8388607, 0);
COMMIT;

SET FOREIGN_KEY_CHECKS = 1;

/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage_world

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-13 15:40:02
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for character_buffs
-- ----------------------------
DROP TABLE IF EXISTS `character_buffs`;
CREATE TABLE `character_buffs` (
  `id` int(8) NOT NULL,
  `character_id` int(8) DEFAULT NULL,
  `buff_id` int(8) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of character_buffs
-- ----------------------------
INSERT INTO `character_buffs` VALUES ('1', '1', '1851');
INSERT INTO `character_buffs` VALUES ('2', '1', '1852');
INSERT INTO `character_buffs` VALUES ('3', '2', '1851');
INSERT INTO `character_buffs` VALUES ('4', '2', '1852');
INSERT INTO `character_buffs` VALUES ('5', '5', '1849');
INSERT INTO `character_buffs` VALUES ('7', '6', '1849');
INSERT INTO `character_buffs` VALUES ('9', '9', '1853');
INSERT INTO `character_buffs` VALUES ('10', '9', '1854');
INSERT INTO `character_buffs` VALUES ('11', '10', '1853');
INSERT INTO `character_buffs` VALUES ('12', '10', '1854');
INSERT INTO `character_buffs` VALUES ('13', '15', '1847');
INSERT INTO `character_buffs` VALUES ('14', '15', '1848');
INSERT INTO `character_buffs` VALUES ('15', '16', '1847');
INSERT INTO `character_buffs` VALUES ('16', '16', '1848');
INSERT INTO `character_buffs` VALUES ('17', '5', '4592');
INSERT INTO `character_buffs` VALUES ('18', '6', '4592');
SET FOREIGN_KEY_CHECKS=1;

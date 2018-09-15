/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage_world

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-13 15:39:48
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for character_default_skills
-- ----------------------------
DROP TABLE IF EXISTS `character_default_skills`;
CREATE TABLE `character_default_skills` (
  `character_id` int(11) DEFAULT NULL,
  `default_skill_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of character_default_skills
-- ----------------------------
INSERT INTO `character_default_skills` VALUES ('13', '156');
INSERT INTO `character_default_skills` VALUES ('14', '156');
INSERT INTO `character_default_skills` VALUES ('15', '164');
INSERT INTO `character_default_skills` VALUES ('16', '164');
INSERT INTO `character_default_skills` VALUES ('1', '161');
INSERT INTO `character_default_skills` VALUES ('2', '161');
INSERT INTO `character_default_skills` VALUES ('3', '160');
INSERT INTO `character_default_skills` VALUES ('4', '160');
INSERT INTO `character_default_skills` VALUES ('5', '162');
INSERT INTO `character_default_skills` VALUES ('6', '162');
INSERT INTO `character_default_skills` VALUES ('9', '163');
INSERT INTO `character_default_skills` VALUES ('10', '163');
INSERT INTO `character_default_skills` VALUES ('3', '169');
INSERT INTO `character_default_skills` VALUES ('4', '169');
INSERT INTO `character_default_skills` VALUES ('5', '166');
INSERT INTO `character_default_skills` VALUES ('6', '166');
INSERT INTO `character_default_skills` VALUES ('9', '167');
INSERT INTO `character_default_skills` VALUES ('10', '167');
INSERT INTO `character_default_skills` VALUES ('13', '170');
INSERT INTO `character_default_skills` VALUES ('14', '170');
INSERT INTO `character_default_skills` VALUES ('15', '168');
INSERT INTO `character_default_skills` VALUES ('16', '168');
INSERT INTO `character_default_skills` VALUES ('1', '165');
INSERT INTO `character_default_skills` VALUES ('2', '165');
SET FOREIGN_KEY_CHECKS=1;

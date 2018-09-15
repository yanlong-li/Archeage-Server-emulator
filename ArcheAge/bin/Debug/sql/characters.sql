/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage_world

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-13 16:56:23
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for characters
-- ----------------------------
DROP TABLE IF EXISTS `characters`;
CREATE TABLE `characters` (
  `id` int(8) NOT NULL,
  `char_race_id` int(8) DEFAULT NULL,
  `char_gender_id` int(8) DEFAULT NULL,
  `model_id` int(8) DEFAULT NULL,
  `faction_id` int(8) DEFAULT NULL,
  `starting_zone_id` int(8) DEFAULT NULL,
  `preview_body_pack_id` int(8) DEFAULT NULL,
  `preview_cloth_pack_id` int(8) DEFAULT NULL,
  `default_return_district_id` int(8) DEFAULT NULL,
  `default_resurrection_district_id` int(8) DEFAULT NULL,
  `default_system_voice_sound_pack_id` int(8) DEFAULT NULL,
  `default_fx_voice_sound_pack_id` int(8) DEFAULT NULL,
  `creatable` int(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of characters
-- ----------------------------
INSERT INTO `characters` VALUES ('1', '1', '1', '10', '101', '179', '3', '183', '342', '343', '86', '0', '0');
INSERT INTO `characters` VALUES ('2', '1', '2', '11', '101', '179', '4', '183', '342', '343', '87', '0', '0');
INSERT INTO `characters` VALUES ('3', '3', '1', '14', '104', '139', '1', '1', '107', '112', '88', null, '1');
INSERT INTO `characters` VALUES ('4', '3', '2', '15', '104', '139', '1', '1', '107', '112', '89', null, '1');
INSERT INTO `characters` VALUES ('5', '4', '1', '16', '103', '129', '6', '184', '186', '187', '90', '0', '0');
INSERT INTO `characters` VALUES ('6', '4', '2', '17', '103', '129', '7', '40', '186', '187', '91', null, '0');
INSERT INTO `characters` VALUES ('8', '2', '2', '13', '101', '23', '1', '1', '1', '30', '92', '0', '1');
INSERT INTO `characters` VALUES ('9', '5', '1', '18', '109', '187', '70', '497', '182', '183', '93', '0', '0');
INSERT INTO `characters` VALUES ('10', '5', '2', '19', '109', '187', '71', '193', '182', '183', '94', null, '0');
INSERT INTO `characters` VALUES ('11', '7', '1', '22', '112', '23', '1', '1', '1', '30', '95', '0', '1');
INSERT INTO `characters` VALUES ('13', '8', '1', '24', '109', '23', '1', '1', '1', '30', '96', '0', '1');
INSERT INTO `characters` VALUES ('15', '6', '1', '20', '113', '184', '31', '185', '184', '185', '97', '0', '0');
INSERT INTO `characters` VALUES ('16', '6', '2', '21', '113', '184', '32', '185', '184', '185', '98', '0', '0');
SET FOREIGN_KEY_CHECKS=1;

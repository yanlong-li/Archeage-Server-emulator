/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-29 02:28:38
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for character_equip_packs
-- ----------------------------
DROP TABLE IF EXISTS `character_equip_packs`;
CREATE TABLE `character_equip_packs` (
  `id` int(11) NOT NULL,
  `ability_id` int(11) DEFAULT NULL,
  `newbie_cloth_pack_id` int(11) DEFAULT NULL,
  `newbie_weapon_pack_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of character_equip_packs
-- ----------------------------
INSERT INTO `character_equip_packs` VALUES ('1', '1', '11', '242');
INSERT INTO `character_equip_packs` VALUES ('2', '2', null, null);
INSERT INTO `character_equip_packs` VALUES ('3', '3', null, null);
INSERT INTO `character_equip_packs` VALUES ('4', '4', null, null);
INSERT INTO `character_equip_packs` VALUES ('5', '5', '11', '520');
INSERT INTO `character_equip_packs` VALUES ('6', '6', '11', '243');
INSERT INTO `character_equip_packs` VALUES ('7', '7', '11', '244');
INSERT INTO `character_equip_packs` VALUES ('8', '8', '11', '243');
INSERT INTO `character_equip_packs` VALUES ('9', '9', null, null);
INSERT INTO `character_equip_packs` VALUES ('10', '10', '11', '245');
SET FOREIGN_KEY_CHECKS=1;

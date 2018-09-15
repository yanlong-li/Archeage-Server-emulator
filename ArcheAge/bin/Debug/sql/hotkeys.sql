/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage_world

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-14 00:36:58
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for hotkeys
-- ----------------------------
DROP TABLE IF EXISTS `hotkeys`;
CREATE TABLE `hotkeys` (
  `id` int(8) NOT NULL,
  `action_id` int(8) DEFAULT NULL,
  `action_type1_id` int(8) DEFAULT NULL,
  `action_type2_id` int(8) DEFAULT NULL,
  `category_id` int(8) DEFAULT NULL,
  `mode_id` int(8) DEFAULT NULL,
  `key_primary` varchar(255) DEFAULT NULL,
  `key_second` varchar(255) DEFAULT NULL,
  `activation` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of hotkeys
-- ----------------------------
INSERT INTO `hotkeys` VALUES ('1', '0', '4', '8', '0', '0', 'w', 'up', '1');
INSERT INTO `hotkeys` VALUES ('2', '1', '4', '0', '0', '0', 's', 'down', '1');
INSERT INTO `hotkeys` VALUES ('3', '2', '4', '0', '0', '0', 'q', '', '1');
INSERT INTO `hotkeys` VALUES ('4', '3', '4', '0', '0', '0', 'e', '', '1');
INSERT INTO `hotkeys` VALUES ('5', '4', '4', '0', '0', '0', 'a', 'left', '1');
INSERT INTO `hotkeys` VALUES ('6', '5', '4', '0', '0', '0', 'd', 'right', '1');
INSERT INTO `hotkeys` VALUES ('7', '6', '2', '0', '0', '0', 'space', '', '1');
INSERT INTO `hotkeys` VALUES ('8', '7', '2', '0', '0', '0', 'x', '', '1');
INSERT INTO `hotkeys` VALUES ('9', '8', '4', '0', '0', '0', 'mousedx', '', '1');
INSERT INTO `hotkeys` VALUES ('10', '9', '4', '0', '0', '0', 'mousedy', '', '1');
INSERT INTO `hotkeys` VALUES ('11', '10', '2', '0', '0', '0', 'wheelup', '', '1');
INSERT INTO `hotkeys` VALUES ('12', '11', '2', '0', '0', '0', 'wheeldown', '', '1');
INSERT INTO `hotkeys` VALUES ('13', '12', '2', '0', '0', '0', 'enter', '', '1');
INSERT INTO `hotkeys` VALUES ('14', '13', '2', '0', '0', '0', 'escape', '', '1');
INSERT INTO `hotkeys` VALUES ('15', '14', '2', '0', '0', '0', 'i', '', '1');
INSERT INTO `hotkeys` VALUES ('16', '15', '2', '0', '0', '0', 'k', '', '1');
INSERT INTO `hotkeys` VALUES ('17', '16', '2', '0', '0', '0', 'c', '', '1');
INSERT INTO `hotkeys` VALUES ('18', '17', '2', '0', '0', '0', 'l', '', '1');
INSERT INTO `hotkeys` VALUES ('19', '18', '2', '0', '0', '0', 'o', '', '1');
INSERT INTO `hotkeys` VALUES ('20', '19', '2', '0', '0', '0', 'SHIFT-m', '', '1');
INSERT INTO `hotkeys` VALUES ('21', '20', '2', '0', '0', '0', 'p', '', '1');
INSERT INTO `hotkeys` VALUES ('22', '21', '2', '0', '0', '0', 'numlock', 'mouse4', '1');
INSERT INTO `hotkeys` VALUES ('23', '22', '2', '0', '0', '0', 'tab', '', '1');
INSERT INTO `hotkeys` VALUES ('24', '23', '2', '0', '0', '0', 'SHIFT-tab', '', '1');
INSERT INTO `hotkeys` VALUES ('25', '24', '2', '0', '0', '0', 'CTRL-tab', '', '1');
INSERT INTO `hotkeys` VALUES ('26', '25', '2', '0', '0', '0', 'CTRL-SHIFT-tab', '', '1');
INSERT INTO `hotkeys` VALUES ('27', '26', '2', '0', '0', '0', 'f12', '', '1');
INSERT INTO `hotkeys` VALUES ('28', '27', '2', '0', '0', '0', 'CTRL-f12', '', '1');
INSERT INTO `hotkeys` VALUES ('29', '28', '2', '0', '0', '0', 'CTRL-v', '', '1');
INSERT INTO `hotkeys` VALUES ('30', '29', '2', '0', '0', '0', 'z', '', '1');
INSERT INTO `hotkeys` VALUES ('31', '30', '2', '0', '0', '0', 'm', '', '1');
INSERT INTO `hotkeys` VALUES ('32', '31', '2', '0', '0', '0', 'f', '', '1');
INSERT INTO `hotkeys` VALUES ('33', '32', '2', '0', '0', '0', 'g', '', '1');
INSERT INTO `hotkeys` VALUES ('34', '33', '2', '0', '0', '0', 'h', '', '1');
INSERT INTO `hotkeys` VALUES ('35', '34', '2', '0', '0', '0', 'j', '', '1');
INSERT INTO `hotkeys` VALUES ('36', '35', '2', '0', '0', '0', '', '', '1');
INSERT INTO `hotkeys` VALUES ('37', '36', '2', '0', '0', '0', 'CTRL-e', '', '1');
INSERT INTO `hotkeys` VALUES ('38', '37', '2', '0', '0', '0', '.', '', '1');
INSERT INTO `hotkeys` VALUES ('39', '38', '2', '0', '0', '0', '', '', '1');
INSERT INTO `hotkeys` VALUES ('40', '39', '2', '0', '0', '0', 'CTRL-f', '', '1');
INSERT INTO `hotkeys` VALUES ('41', '40', '2', '0', '0', '0', '', '', '1');
INSERT INTO `hotkeys` VALUES ('42', '41', '2', '0', '0', '0', 'f1', '', '1');
INSERT INTO `hotkeys` VALUES ('43', '42', '2', '0', '5', '0', 'f2 1,f3 2,f4 3,f5 4', '', '1');
INSERT INTO `hotkeys` VALUES ('44', '43', '2', '0', '5', '0', 'f6 1,f7 2,f8 3', '', '1');
INSERT INTO `hotkeys` VALUES ('45', '44', '2', '0', '0', '0', '', '', '1');
INSERT INTO `hotkeys` VALUES ('46', '45', '2', '0', '0', '0', 'SHIFT-n', '', '1');
INSERT INTO `hotkeys` VALUES ('47', '46', '2', '0', '0', '0', 'SHIFT-/', '', '1');
INSERT INTO `hotkeys` VALUES ('48', '47', '2', '0', '0', '0', 'f11', '', '1');
INSERT INTO `hotkeys` VALUES ('49', '48', '2', '0', '0', '0', 'f10', '', '1');
INSERT INTO `hotkeys` VALUES ('50', '49', '2', '0', '0', '0', 'insert', '', '1');
INSERT INTO `hotkeys` VALUES ('51', '50', '2', '0', '0', '0', 'delete', '', '1');
INSERT INTO `hotkeys` VALUES ('52', '51', '2', '0', '0', '0', 'home', '', '1');
INSERT INTO `hotkeys` VALUES ('53', '52', '2', '0', '0', '0', 'end', '', '1');
INSERT INTO `hotkeys` VALUES ('54', '53', '2', '0', '0', '3', 'wheelup', '', '1');
INSERT INTO `hotkeys` VALUES ('55', '54', '2', '0', '0', '3', 'wheeldown', '', '1');
INSERT INTO `hotkeys` VALUES ('56', '55', '2', '0', '0', '3', 'CTRL-insert', '', '1');
INSERT INTO `hotkeys` VALUES ('57', '56', '2', '0', '3', '0', 'CTRL-delete', '', '1');
INSERT INTO `hotkeys` VALUES ('58', '57', '2', '0', '0', '3', 'CTRL-home', '', '1');
INSERT INTO `hotkeys` VALUES ('59', '58', '2', '0', '0', '3', 'CTRL-end', '', '1');
INSERT INTO `hotkeys` VALUES ('60', '59', '2', '0', '0', '3', 'CTRL-pageup', '', '1');
INSERT INTO `hotkeys` VALUES ('61', '60', '2', '0', '0', '3', 'CTRL-pagedown', '', '1');
INSERT INTO `hotkeys` VALUES ('62', '61', '2', '0', '0', '0', 'ALT-insert', '', '1');
INSERT INTO `hotkeys` VALUES ('63', '62', '2', '0', '0', '0', 'ALT-f5', '', '1');
INSERT INTO `hotkeys` VALUES ('64', '63', '2', '0', '0', '0', 'ALT-f6', '', '1');
INSERT INTO `hotkeys` VALUES ('65', '64', '2', '0', '0', '0', 'ALT-f7', '', '1');
INSERT INTO `hotkeys` VALUES ('66', '65', '2', '0', '0', '0', 'ALT-f8', '', '1');
INSERT INTO `hotkeys` VALUES ('67', '66', '2', '0', '0', '0', 'ALT-home', '', '1');
INSERT INTO `hotkeys` VALUES ('68', '67', '2', '0', '0', '0', 'ALT-end', '', '1');
INSERT INTO `hotkeys` VALUES ('69', '68', '2', '0', '0', '0', 'ALT-pageup', '', '1');
INSERT INTO `hotkeys` VALUES ('70', '69', '2', '0', '0', '0', 'ALT-pagedown', '', '1');
INSERT INTO `hotkeys` VALUES ('71', '70', '2', '0', '0', '0', 'pagedown', '', '1');
INSERT INTO `hotkeys` VALUES ('72', '71', '2', '0', '0', '0', 'pageup', '', '1');
INSERT INTO `hotkeys` VALUES ('73', '72', '2', '0', '0', '0', '', '', '1');
INSERT INTO `hotkeys` VALUES ('74', '73', '2', '0', '0', '0', 'CTRL-i', '', '1');
INSERT INTO `hotkeys` VALUES ('75', '74', '2', '0', '0', '0', 'CTRL-r', '', '1');
INSERT INTO `hotkeys` VALUES ('76', '75', '2', '0', '0', '0', 'SHIFT-r', '', '1');
INSERT INTO `hotkeys` VALUES ('77', '76', '2', '0', '0', '0', 'SHIFT-t', '', '1');
INSERT INTO `hotkeys` VALUES ('78', '77', '2', '0', '0', '0', 'SHIFT-v', '', '1');
INSERT INTO `hotkeys` VALUES ('79', '78', '1', '2', '0', '0', 'CTRL-SHIFT-r', '', '1');
INSERT INTO `hotkeys` VALUES ('80', '79', '1', '2', '0', '0', 'ALT-SHIFT-r', '', '1');
INSERT INTO `hotkeys` VALUES ('81', '80', '2', '0', '0', '0', 'CTRL-q', '', '1');
INSERT INTO `hotkeys` VALUES ('82', '81', '2', '0', '0', '0', '', '', '1');
INSERT INTO `hotkeys` VALUES ('83', '82', '1', '2', '5', '0', '1 1,2 2,3 3,4 4,5 5,6 6,7 7,8 8,9 9,0 10,minus 11,equals 12,SHIFT-1 13,SHIFT-2 14,SHIFT-3 15,SHIFT-4 16,SHIFT-5 17,SHIFT-6 18,SHIFT-7 19,SHIFT-8 20,SHIFT-9 21,SHIFT-0 22,SHIFT-minus 23,SHIFT-equals 24', '', '1');
INSERT INTO `hotkeys` VALUES ('84', '83', '2', '0', '0', '0', 'SHIFT-b', '', '1');
INSERT INTO `hotkeys` VALUES ('85', '84', '1', '2', '5', '0', '', '', '1');
INSERT INTO `hotkeys` VALUES ('86', '85', '1', '2', '5', '1', 'r 1,t 2,y 3,u 4', '', '1');
INSERT INTO `hotkeys` VALUES ('87', '86', '2', '0', '0', '0', 'SHIFT-up', '', '1');
INSERT INTO `hotkeys` VALUES ('88', '87', '2', '0', '0', '0', 'SHIFT-down', '', '1');
INSERT INTO `hotkeys` VALUES ('89', '88', '2', '0', '5', '0', '', '', '1');
INSERT INTO `hotkeys` VALUES ('90', '89', '2', '0', '0', '0', '', '', '1');
INSERT INTO `hotkeys` VALUES ('91', '90', '2', '0', '0', '2', 'wheelup', '', '1');
INSERT INTO `hotkeys` VALUES ('92', '91', '2', '0', '0', '2', 'wheeldown', '', '1');
INSERT INTO `hotkeys` VALUES ('93', '92', '2', '0', '0', '2', 'ALT-wheelup', '', '1');
INSERT INTO `hotkeys` VALUES ('94', '93', '2', '0', '0', '2', 'ALT-wheeldown', '', '1');
INSERT INTO `hotkeys` VALUES ('95', '94', '2', '0', '0', '2', 'SHIFT-wheelup', '', '1');
INSERT INTO `hotkeys` VALUES ('96', '95', '2', '0', '0', '2', 'SHIFT-wheeldown', '', '1');
INSERT INTO `hotkeys` VALUES ('97', '96', '2', '0', '0', '2', 'CTRL-wheelup', '', '1');
INSERT INTO `hotkeys` VALUES ('98', '97', '2', '0', '0', '2', 'CTRL-wheeldown', '', '1');
INSERT INTO `hotkeys` VALUES ('99', '98', '2', '0', '0', '0', 'n', '', '1');
INSERT INTO `hotkeys` VALUES ('100', '99', '2', '0', '0', '0', 'SHIFT-k', '', '1');
INSERT INTO `hotkeys` VALUES ('101', '100', '2', '0', '0', '0', 'SHIFT-u', '', '1');
INSERT INTO `hotkeys` VALUES ('102', '101', '2', '0', '0', '0', 'SHIFT-y', '', '1');
INSERT INTO `hotkeys` VALUES ('103', '102', '2', '0', '5', '4', 'd 1,f 2,g 3', '', '1');
INSERT INTO `hotkeys` VALUES ('104', '103', '2', '0', '0', '0', '', '', '1');
INSERT INTO `hotkeys` VALUES ('105', '104', '2', '0', '0', '0', '', '', '1');
INSERT INTO `hotkeys` VALUES ('106', '105', '2', '0', '0', '0', '/', '', '0');
SET FOREIGN_KEY_CHECKS=1;

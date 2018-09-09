/*
Navicat MySQL Data Transfer

Source Server         : AA
Source Server Version : 80011
Source Host           : localhost:3306
Source Database       : archeage

Target Server Type    : MYSQL
Target Server Version : 80011
File Encoding         : 65001

Date: 2018-09-07 19:42:11
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for itemset
-- ----------------------------
DROP TABLE IF EXISTS `itemset`;
CREATE TABLE `itemset` (
  `id` int(11) NOT NULL,
  `name` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_general_ci DEFAULT NULL,
  `head` int(255) DEFAULT NULL,
  `chest` int(255) DEFAULT NULL,
  `legs` int(255) DEFAULT NULL,
  `gloves` int(255) DEFAULT NULL,
  `feet` int(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of itemset
-- ----------------------------
INSERT INTO `itemset` VALUES ('149', 'Безумие сумасшедшего отшельника', '24503', '24504', '24505', '24506', '24507');
INSERT INTO `itemset` VALUES ('159', 'Безжалостный кошмар', '27293', '27294', '27298', '27296', '27299');
INSERT INTO `itemset` VALUES ('166', 'Воля забытого экипажа', '27996', '27995', '27998', '27994', '27997');
SET FOREIGN_KEY_CHECKS=1;

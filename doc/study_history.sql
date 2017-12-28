CREATE DATABASE  IF NOT EXISTS `study` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `study`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 192.168.10.94    Database: study
-- ------------------------------------------------------
-- Server version	5.7.18

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `history`
--

DROP TABLE IF EXISTS `history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `history` (
  `identity` varchar(20) CHARACTER SET latin1 NOT NULL,
  `drugrelated` varchar(1) CHARACTER SET latin1 DEFAULT NULL,
  `inspect` varchar(1) CHARACTER SET latin1 DEFAULT '1',
  `fullmark` varchar(1) CHARACTER SET latin1 DEFAULT NULL,
  `licensetype` varchar(1) CHARACTER SET latin1 NOT NULL,
  `name` varchar(45) CHARACTER SET latin1 NOT NULL,
  `noticedate` datetime DEFAULT NULL,
  `phone` varchar(45) CHARACTER SET latin1 NOT NULL,
  `stoplicense` varchar(1) CHARACTER SET latin1 DEFAULT NULL,
  `completed` varchar(1) CHARACTER SET latin1 DEFAULT NULL,
  `studylog` varchar(500) CHARACTER SET latin1 DEFAULT NULL,
  `startdate` datetime DEFAULT NULL,
  `syncdate` datetime NOT NULL,
  `wechat` varchar(45) CHARACTER SET latin1 DEFAULT NULL,
  `ordinal` int(11) NOT NULL DEFAULT '1',
  `finishdate` datetime NOT NULL,
  PRIMARY KEY (`ordinal`),
  UNIQUE KEY `ordinal_UNIQUE` (`ordinal`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-06-24  9:15:26

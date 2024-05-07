USE `mydb`;

/*Получение списков*/
DROP procedure IF EXISTS `get_countries`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_countries`()
BEGIN
	SELECT  `id`,`name`,`picturepath` FROM `countries`;
END$$
DELIMITER ;

DROP procedure IF EXISTS `get_ships_by_country_id`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_ships_by_country_id`(IN `country_id` INT)
BEGIN
	SELECT `id`, `name`, `picturepath`
    FROM `ships`
    WHERE `countries_id` = `country_id`;
END$$
DELIMITER ;

DROP procedure IF EXISTS `get_commanders_by_country_id`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_commanders_by_country_id`(IN `country_id` INT)
BEGIN
 	SELECT `id`, `name`, `picturepath`
    FROM `special_commanders`
    WHERE `countries_id` = `country_id`;
END$$
DELIMITER ;

DROP procedure IF EXISTS `get_maps`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_maps`()
BEGIN
	SELECT  `id`,`name`,`picturepath` FROM `maps`;
END$$
DELIMITER ;

DROP procedure IF EXISTS `get_player_levels`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_player_levels`()
BEGIN
	SELECT  `id`,`name`,`picturepath` FROM `player_levels`;
END$$
DELIMITER ;

DROP procedure IF EXISTS `get_achievements`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_achievements`()
BEGIN
	SELECT  `id`,`name`,`picturepath` FROM `achievements`;
END$$
DELIMITER ;

DROP procedure IF EXISTS `get_containers`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_containers`()
BEGIN
	SELECT  `id`,`name`,`picturepath` FROM `containers`;
END$$
DELIMITER ;

/*Получение объектов*/
DROP procedure IF EXISTS `get_country`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_country`(IN `object_id` INT)
BEGIN
	SELECT  `name`,`description`,`picturepath` 
    FROM `countries`
    WHERE `id` = `object_id`;
END$$
DELIMITER ;

DROP procedure IF EXISTS `get_ship`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_ship`(IN `object_id` INT)
BEGIN
	SELECT  `ships`.`name` AS `ship_name`, `ships`.`description` AS `ship_description`, `ships`.`picturepath` AS `ship_picturepath`,`ships`.`level` AS `ship_level`,`ships`.`survivability` AS `ship_survivability`,`ships`.`aircraft` AS `ship_aircraft`,`ships`.`artillery` AS `ship_artillery`,`ships`.`torpedoes` AS `ship_torpedoes`,`ships`.`airdefense` AS `ship_airdefense`,`ships`.`maneuverability` AS `ship_maneuverability`,`ships`.`concealment` AS `ship_concealment`,`ships`.`pricemoney` AS `ship_pricemoney`,`ships`.`priceexp` AS `ship_priceexp`,
			`ship_class`.`name` AS `ship_class_name`,`ship_class`.`picturepath` AS `ship_class_picturepath`,
            `modules`.`name` AS `modules_name`, `modules`.`description` AS `modules_description`, `modules`.`picturepath` AS `modules_picturepath`,`modules`.`pricemoney` AS `modules_pricemoney`,`modules`.`priceexp` AS `modules_priceexp`
    FROM `ships`
    LEFT JOIN  `ship_class` ON `ships`.`shipclass_id` = `ship_class`.`id`
	LEFT JOIN  `modules` ON `modules`.`ships_id` = `ships`.`id`
    WHERE `ships`.`id` = `object_id`;
END$$
DELIMITER ;

DROP procedure IF EXISTS `get_commander`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_commander`(IN `object_id` INT)
BEGIN
	SELECT  `special_commanders`.`name` AS `special_commanders_name`,`special_commanders`.`description`AS `special_commanders_description`,`special_commanders`.`picturepath`AS `special_commanders_picturepath`, `special_commanders`.`origins`AS `special_commanders_origins`,
			`talents`.`name` AS `talents_name`,`talents`.`description` AS `talents_description`,`talents`.`picturepath` AS `talents_picturepath`
    FROM `special_commanders`
	LEFT JOIN 
		`talents` ON `talents`.`specialcommanders_id` = `special_commanders`.`id`
	WHERE `special_commanders`.`id` = `object_id`;
END$$
DELIMITER ;

DROP procedure IF EXISTS `get_map`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_map`(IN `object_id` INT)
BEGIN
	SELECT  `name`,`description`,`picturepath`,`battletiers`,`size`,`replyfilename` 
    FROM `maps`
    WHERE `id` = `object_id`;
END$$
DELIMITER ;

DROP procedure IF EXISTS `get_player_level`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_player_level`(IN `object_id` INT)
BEGIN
	SELECT  `name`,`description`,`picturepath`,`battleneed`, `battletotal`
    FROM `player_levels`
    WHERE `id` = `object_id`;
END$$
DELIMITER ;

DROP procedure IF EXISTS `get_achievement`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_achievement`(IN `object_id` INT)
BEGIN
	SELECT  `type_achievements`.`name` AS `type_achievement_name`, `achievements`.`name` AS `achievement_name` ,`description`,`picturepath` 
    FROM `achievements`
    JOIN `type_achievements` ON `achievements`.`typeachivements_id` = `type_achievements`.`id`
	WHERE `achievements`.`id` = 0 ;
END$$
DELIMITER ;

DROP procedure IF EXISTS `get_container`;
DELIMITER $$
USE `mydb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_container`(IN `object_id` INT)
BEGIN
	SELECT  `containers`.`name` AS `container_name`, `containers`.`description` AS `container_description`, `containers`.`picturepath`  AS `container_picturepath`,
			`loot`.`chance` AS `loot_chance`,
            `items`.`name` AS `item_name`,
            `type_item`.`name` AS `type_item_name`
    FROM `containers`
	JOIN `loot` ON `loot`.`containers_id` = `containers`.`id`
	JOIN `items` ON `loot`.`items_id` = `items`.`id`
	JOIN `type_item` ON `items`.`typeitem_id` = `type_item`.`id`
    WHERE `containers`.`id` = `object_id`;
END$$
DELIMITER ;

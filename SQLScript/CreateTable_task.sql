CREATE TABLE `task_db`.`task` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `parentId` INT NOT NULL DEFAULT 0 COMMENT '父Id\n0為自己是工作主項\n1以上是工作細項',
  `title` VARCHAR(50) NOT NULL COMMENT '工作任務名稱',
  `start` DATETIME NOT NULL COMMENT '開始時間',
  `end` DATETIME NOT NULL COMMENT '結束時間',
  `progress` INT NULL DEFAULT 0 COMMENT '進度百分比',
  `count` INT NULL DEFAULT 0 COMMENT '數量',
  PRIMARY KEY (`id`));

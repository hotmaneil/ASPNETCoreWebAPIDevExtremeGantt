CREATE TABLE `task_db`.`resourceassignment` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `taskId` INT NULL,
  `resourceId` INT NULL COMMENT '工作人員Id',
  PRIMARY KEY (`id`));

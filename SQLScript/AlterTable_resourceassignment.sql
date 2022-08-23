ALTER TABLE `task_db`.`resourceassignment` 
CHANGE COLUMN `taskId` `taskId` INT(11) NOT NULL ,
CHANGE COLUMN `resourceId` `resourceId` INT(11) NOT NULL COMMENT '工作人員Id' ;

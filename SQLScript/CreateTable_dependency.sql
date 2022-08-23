CREATE TABLE `task_db`.`dependency` (
  `id` INT NOT NULL,
  `predecessorId` INT NOT NULL,
  `successorId` INT NOT NULL,
  `type` INT NULL,
  PRIMARY KEY (`id`))
COMMENT = '工作相依(延伸)關係';

--------------------------------------------------------
--  文件已创建 - 星期二-五月-09-2017   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table VITALLOG
--------------------------------------------------------

  CREATE TABLE "CITY"."VITALLOG" 
   (	"USERNAME" VARCHAR2(50 BYTE), 
	"TIME" TIMESTAMP (6), 
	"KEYWORD" VARCHAR2(20 BYTE), 
	"IP" VARCHAR2(20 BYTE), 
	"OPERATION" VARCHAR2(2048 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  Constraints for Table VITALLOG
--------------------------------------------------------

  ALTER TABLE "CITY"."VITALLOG" MODIFY ("OPERATION" NOT NULL ENABLE);
  ALTER TABLE "CITY"."VITALLOG" MODIFY ("KEYWORD" NOT NULL ENABLE);
  ALTER TABLE "CITY"."VITALLOG" MODIFY ("IP" NOT NULL ENABLE);
  ALTER TABLE "CITY"."VITALLOG" MODIFY ("TIME" NOT NULL ENABLE);
  ALTER TABLE "CITY"."VITALLOG" MODIFY ("USERNAME" NOT NULL ENABLE);

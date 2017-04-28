--------------------------------------------------------
--  文件已创建 - 星期五-四月-28-2017   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table CONFIG
--------------------------------------------------------

  CREATE TABLE "CITY"."CONFIG" 
   (	"COUNTYCODE" VARCHAR2(20 BYTE), 
	"BUSINESSTABLENAME" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
REM INSERTING into CITY.CONFIG
SET DEFINE OFF;
Insert into CITY.CONFIG (COUNTYCODE,BUSINESSTABLENAME) values ('fushan','fushanbusiness');
Insert into CITY.CONFIG (COUNTYCODE,BUSINESSTABLENAME) values ('haiyang','haiyangbusiness');
--------------------------------------------------------
--  DDL for Index CONFIG_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "CITY"."CONFIG_PK" ON "CITY"."CONFIG" ("COUNTYCODE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  Constraints for Table CONFIG
--------------------------------------------------------

  ALTER TABLE "CITY"."CONFIG" MODIFY ("COUNTYCODE" NOT NULL ENABLE);
  ALTER TABLE "CITY"."CONFIG" MODIFY ("BUSINESSTABLENAME" NOT NULL ENABLE);
  ALTER TABLE "CITY"."CONFIG" ADD CONSTRAINT "CONFIG_PK" PRIMARY KEY ("COUNTYCODE")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;

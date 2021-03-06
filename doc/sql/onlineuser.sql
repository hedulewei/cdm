--------------------------------------------------------
--  文件已创建 - 星期一-六月-12-2017   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table ONLINEUSER
--------------------------------------------------------

  CREATE TABLE "CITY"."ONLINEUSER" 
   (	"IDENTITY" VARCHAR2(20 BYTE), 
	"NAME" VARCHAR2(50 BYTE), 
	"PHONE" VARCHAR2(20 BYTE), 
	"WECHAT" VARCHAR2(50 BYTE), 
	"LOG" VARCHAR2(2000 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  DDL for Index ONLINEUSER_PK1
--------------------------------------------------------

  CREATE UNIQUE INDEX "CITY"."ONLINEUSER_PK1" ON "CITY"."ONLINEUSER" ("IDENTITY") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  Constraints for Table ONLINEUSER
--------------------------------------------------------

  ALTER TABLE "CITY"."ONLINEUSER" MODIFY ("WECHAT" NOT NULL ENABLE);
  ALTER TABLE "CITY"."ONLINEUSER" MODIFY ("PHONE" NOT NULL ENABLE);
  ALTER TABLE "CITY"."ONLINEUSER" MODIFY ("NAME" NOT NULL ENABLE);
  ALTER TABLE "CITY"."ONLINEUSER" MODIFY ("IDENTITY" NOT NULL ENABLE);
  ALTER TABLE "CITY"."ONLINEUSER" ADD CONSTRAINT "ONLINEUSER_PK" PRIMARY KEY ("IDENTITY")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;

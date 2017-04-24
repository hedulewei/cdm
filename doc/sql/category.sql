--------------------------------------------------------
--  �ļ��Ѵ��� - ������-����-14-2017   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table CATEGORIES
--------------------------------------------------------

  CREATE TABLE "CITY"."CATEGORIES" 
   (	"CATEGORY" VARCHAR2(20 BYTE), 
	"NAME" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
REM INSERTING into CITY.CATEGORIES
SET DEFINE OFF;
Insert into CITY.CATEGORIES (CATEGORY,NAME) values ('cars','������ҵ��');
Insert into CITY.CATEGORIES (CATEGORY,NAME) values ('drivers','��ʻ֤ҵ��');
Insert into CITY.CATEGORIES (CATEGORY,NAME) values ('archives','����ҵ��');
--------------------------------------------------------
--  DDL for Index CATEGORIES_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "CITY"."CATEGORIES_PK" ON "CITY"."CATEGORIES" ("CATEGORY") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS" ;
--------------------------------------------------------
--  Constraints for Table CATEGORIES
--------------------------------------------------------

  ALTER TABLE "CITY"."CATEGORIES" ADD CONSTRAINT "CATEGORIES_PK" PRIMARY KEY ("CATEGORY")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERS"  ENABLE;
  ALTER TABLE "CITY"."CATEGORIES" MODIFY ("CATEGORY" NOT NULL ENABLE);
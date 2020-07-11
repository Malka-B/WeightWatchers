
/* TableNameVariable */

/* DropTable */

declare
  n number(10);
begin
  select count(*) into n from user_tables where table_name = 'SUBSCRIBERSAGA';
  if(n > 0)
  then
    execute immediate 'drop table "SUBSCRIBERSAGA"';
  end if;
end;

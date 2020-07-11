
/* TableNameVariable */

set @tableNameQuoted = concat('`', @tablePrefix, 'SubscriberSaga`');
set @tableNameNonQuoted = concat(@tablePrefix, 'SubscriberSaga');


/* DropTable */

set @dropTable = concat('drop table if exists ', @tableNameQuoted);
prepare script from @dropTable;
execute script;
deallocate prepare script;

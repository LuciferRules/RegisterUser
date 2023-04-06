-- Backup database to physical location with date as filename
DECLARE @FileName varchar(1000)
SELECT @FileName = (SELECT 'C:\Users\yiptu\source\repos\backup_registerdata\registerdata' + convert(varchar(500), GetDate(),112) + '.bak')
BACKUP DATABASE registerdata TO DISK=@FileName;

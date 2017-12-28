ftp -v -n 192.168.8.244 2>>ftplog 1>>ftplog 0>>ftplog << END
user fyang ycl1mail
type binary
prompt
cd upload
lcd /home/fyang/cron
mget * 
bye
END

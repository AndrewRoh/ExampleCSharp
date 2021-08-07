
* "defer_build":true 백그라운드에서 인덱스를 생성함
* primary key 존재하는지 체크
```mysql
CREATE PRIMARY INDEX `beer_primary` ON `beer-sample` USING GSI;
select * from `beer-sample` data where type='beer' order by meta().id limit 10 offset 0

CREATE PRIMARY INDEX `beer_primary` ON `beer-sample` WITH { "defer_build":true }
select * from `beer-sample`.`_default`.`_default` data where type='beer' order by meta().id limit 10 offset 0

CREATE PRIMARY INDEX `def_primary` ON `travel-sample` WITH {  "defer_build":true }
CREATE PRIMARY INDEX idx_airport_primary ON airport USING GSI;
SELECT * FROM system:indexes WHERE name="idx_hotel_primary";


SELEC `kr-center-logs_primary` ON `kr-center-logs` WITH {  "defer_build":true }

CREATE PRIMARY INDEX `kr-center-logs_primary` ON `kr-center-logs` USING GSI;
select * from `kr-center-logs` data where kind='t_error' order by meta().id limit 10 offset 100

CREATE PRIMARY INDEX `kr-regio1-logs_primary` ON `kr-region1-logs` USING GSI;
select * from `kr-region1-logs` where LogKind=12 and date > '2021-07-01 00:00:00' and item_id=303002000 order by meta().date limit 500 offset 500

18초

CREATE INDEX `kr-regio1-logs_idx_consume` ON `kr-region1-logs`(`date`,`item_id`) WHERE(`LogKind`=12) USING GSI;

select * from `kr-region1-logs` USE INDEX (`kr-regio1-logs_idx_consume`) where LogKind=12 and date > '2021-07-01 00:00:00' and item_id=303002000 order by meta().date limit 500 offset 500

select data.* from `kr-region1-logs` data where LogKind=12 and date > '2021-07-01 00:00:00' and item_id=303002000 order by meta().date limit 500 offset 500

/opt/couchbase/bin/cbbackup -u Administrator -p tmxpdj http://127.0.0.1:8091 -b kr-center-logs ~/backup-43/
scp -i ~/stairgames.pem -r ec2-user@3.35.166.192:~/backup-43/* /home/stair/backup-43/
/opt/couchbase/bin/cbrestore ~/backup-43 http://127.0.0.1:8091 -u Administrator -p tmxpdj -b kr-center-logs
--------------------------------------------
 
/opt/couchbase/bin/cbbackup -u Administrator -p tmxpdj http://127.0.0.1:8091 -b kr-region1-logs ~/backup-43/
scp -i ~/stairgames.pem -r ec2-user@3.35.166.192:~/backup-43/* /home/stair/backup-43/
/opt/couchbase/bin/cbrestore ~/backup-43 http://127.0.0.1:8091 -u Administrator -p tmxpdj -b kr-region1-logs

{
  "LogKind": 12,
  "log_uid": null,
  "date": "2021-07-21T14:50:26.458004+09:00",
  "transaction_log_uid": 2021072114502610218,
  "region_index": 1,
  "account_uid": 449,
  "character_uid": 100000000000000632,
  "character_name": "\uB358\uC804\uAC00\uC988\uC544",
  "type": "Get",
  "event_type": "BossReward(dungeon:6000800,level:0)",
  "count": 6,
  "world_type": "Dungeon",
  "channel_index": 6000800,
  "channel_uid": 37,
  "item_uid": 100000000035900049,
  "item_id": 303002000,
  "before_count": 242,
  "after_count": 248,
  "extra_log": "",
  "kind": "t_use_get_consume_item"
}

```

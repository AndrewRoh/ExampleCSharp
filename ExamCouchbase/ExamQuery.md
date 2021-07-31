
* "defer_build":true 백그라운드에서 인덱스를 생성함
* primary key 존재하는지 체크
```mysql
CREATE PRIMARY INDEX `def_primary` ON `travel-sample` WITH {  "defer_build":true }
CREATE PRIMARY INDEX idx_airport_primary ON airport USING GSI;
SELECT * FROM system:indexes WHERE name="idx_hotel_primary";
```

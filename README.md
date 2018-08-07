# Unity CullingMask Test

* Camera.cullingMask の挙動について調べる。
* ビット演算の機微を知る
  * `~` not
  * `<<` left shift
  * `&` and

## キー操作

* `T` : `_10` レイヤーをマスク On/Off トグルする
* `E` : `_11` レイヤーをマスク On/Off トグルする
* `R` : `Everything` 指定。マスクに -1 (= 32bit 1で埋まっている最大値) を代入する。 
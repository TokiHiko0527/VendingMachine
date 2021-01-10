#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

/* define変数で指定 */
#define UpPrice		100
#define DownPrice	150

/*グローバル変数*/
int Totalprice = 0;	//合計値保存用

//----------------------
//※品切れは考慮しないボタンの処理
//----------------------

/*ボタン上端ステータス*/
int UpButton(int All) {
	int n;

	if (All >= UpPrice) {
		n = 1111;
	}else{
		n = 0;
	}
	return n;
}

/*ボタン下端ステータス*/
int DownButton(int All) {
	int m;

	if (All >= DownPrice) {
		m = 1111;
	}else{
		m = 0;
	}
	return m;
}

//---------
//後で10 100 50 500以外ははじく設定に変更
//---------
/*-- 合計値計算 --*/
int Total(int count) {

	Totalprice = Totalprice + count;	//入力金額を足す

	return Totalprice;				//合計値を返す
}

//--------------
//一旦上段と下段にそれぞれ同じ商品が入っているとしてとりあえず作る
//--------------
/* 入力ボタンの判定 */
int Product(int Val) {

	int ProPrice = 0;	//商品の価格用
	int Judge = 0;		//購入可能か判定用


	/*商品価格の判定*/
	if (Val >= 100 && Val < 200) {
		ProPrice = 1;					//100円商品ボタン
	}
	else {
		ProPrice = 2;					//150円商品ボタン
	}

	/*購入可能か判定*/
	//100円商品を選択の場合
	if (ProPrice == 1) {
		//商品購入が可能な場合
		if (Totalprice >= UpPrice) {
			Judge = 1;
		}
		else {
			Judge = 0;
		}
	}
	//150円の商品が選択された場合
	else {
		//150円の商品が購入可能な場合
		if (Totalprice >= DownPrice) {
			Judge = 2;
		}
		else {
			Judge = 0;
		}
	}
	return Judge;
}

//--------------
//やること
//・エラー、例外処理の追加
//・色々決め打ちして作ってるところの修正
//---------------

/*-- 受け取った文字列に固定値つけて返す --*/
int main(void)
{
	//ループ開始
	while (1)
	{

		bool Debugmode = true;		//デバッグモードオンオフ
		char input[16];				//GUIからの入力値
		char Start[8];				//開始待機用
		int EvID;					//イベントID
		int EvValue;				//イベント値
		int UpBtn;					//上段ボタン
		int DownBtn;				//下段ボタン
		int Judge = 3;				//商品購入可能か判断
		int ProductU = 1;			//100円商品購入有無
		int ProductD = 1;			//150円商品購入有無
		int Change = 0;				//おつり金額　ループが始まる段階で0円にする

		//デバッグ用コメント
		if (Debugmode == false) printf("q以外でスタート：");

		//アクションがあるまで待機用 & 終了
		scanf_s("%s", Start, 6);

		if (Start[0] == 'q') {
			break;
		}

		/* ファイルオープン */
		FILE* fp;
		if ((fp = fopen("GUI_File.txt", "r")) == NULL) {
			printf("file open error!!\n");
			exit(EXIT_FAILURE);		//ファイルが読み込めなければ終了				
		}


		/* ファイル読み込み */
		(fgets(input, 7, fp));		//inputに文字列取得

		//デバッグ用コメント
		if(Debugmode == false)	printf("%s\n", input);	//文字列確認用


		//GUIからのファイルクローズ
		fclose(fp);


		/* イベントIDとイベント値の取得 */
		EvID = atoi(strtok(input, ","));			//イベントIDをint型で取得
		//デバッグ用コメント
		if (Debugmode == false)	printf("イベントID：%d\n", EvID);			

		EvValue = atoi(strtok(NULL, ","));			//イベント値をint型で取得
		//デバッグ用コメント
		if (Debugmode == false) printf("イベント値：%d\n", EvValue);


		/* イベントIDの判定 */
		switch (EvID) {
		//イベントID = 1(金額投入)の場合
		case 1:
			EvValue = Total(EvValue);
			break;

		//イベントID = 2(商品ボタン入力)の場合
		case 2:
			Judge = Product(EvValue);
			break;


		//イベントID = 3(おつりレバーが引かれた)の場合
		case 3:
			Change = Totalprice;		//おつり金額を保存
			Totalprice = 0;			//合計金額を0に
			EvValue = 0;			//イベント値をリセット
			break;


		//イベントID対象外
		default:
			break;
		}

		/* 商品購入時用の処理 */
		if (Judge == 0) {
			/* 上段・下段ボタンの判定 */
			UpBtn = UpButton(Totalprice);
			DownBtn = DownButton(Totalprice);
		}
		//100円の商品購入時パラメータ変更
		else if (Judge == 1) {
			UpBtn = 0;
			DownBtn = 0;
			ProductU = 2;
			Change = Totalprice - 100;
			Totalprice = 0;
			EvValue = 0;
		}
		//150円の商品購入時パラメータ変更
		else if(Judge == 2){
			UpBtn = 0;
			DownBtn = 0;
			ProductD = 2;
			Change = Totalprice - 150;
			Totalprice = 0;
			EvValue = 0;
		}
		//イベントID２以外の処理
		else {
			/* 上段・下段ボタンの判定 */
			UpBtn = UpButton(Totalprice);
			DownBtn = DownButton(Totalprice);
		}


		/*Debugmodeで表示内容の変更*/
		if (Debugmode == true) {
			/* GUIに返す値の表示 */
			printf("%04d,%04d,%04d,%02d,%02d,%04d\n", UpBtn, DownBtn, Totalprice,ProductU,ProductD,Change);

			/* GUIに送るファイルの出力 */
			fp = fopen("System_File.txt", "w");
			fprintf(fp, "%04d,%04d,%04d,%02d,%02d,%04d", UpBtn, DownBtn, Totalprice, ProductU, ProductD, Change);
			fclose(fp);
		}
		else {

			/* GUIに返す値の表示 */
			printf("出力値：%04d,%04d,%04d,%02d,%02d,%04d\n", UpBtn, DownBtn, Totalprice,ProductU,ProductD,Change);

			/* GUIに送るファイルの出力 */
			fp = fopen("System_File.txt", "w");
			fprintf(fp, "%04d,%04d,%04d,%02d,%02d,%04d", UpBtn, DownBtn, Totalprice, ProductU, ProductD, Change);
			fclose(fp);
		}
	}
	return 0;
}
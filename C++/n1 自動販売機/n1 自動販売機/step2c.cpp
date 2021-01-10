#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

/* define�ϐ��Ŏw�� */
#define UpPrice		100
#define DownPrice	150

/*�O���[�o���ϐ�*/
int Totalprice = 0;	//���v�l�ۑ��p

//----------------------
//���i�؂�͍l�����Ȃ��{�^���̏���
//----------------------

/*�{�^����[�X�e�[�^�X*/
int UpButton(int All) {
	int n;

	if (All >= UpPrice) {
		n = 1111;
	}else{
		n = 0;
	}
	return n;
}

/*�{�^�����[�X�e�[�^�X*/
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
//���10 100 50 500�ȊO�͂͂����ݒ�ɕύX
//---------
/*-- ���v�l�v�Z --*/
int Total(int count) {

	Totalprice = Totalprice + count;	//���͋��z�𑫂�

	return Totalprice;				//���v�l��Ԃ�
}

//--------------
//��U��i�Ɖ��i�ɂ��ꂼ�ꓯ�����i�������Ă���Ƃ��ĂƂ肠�������
//--------------
/* ���̓{�^���̔��� */
int Product(int Val) {

	int ProPrice = 0;	//���i�̉��i�p
	int Judge = 0;		//�w���\������p


	/*���i���i�̔���*/
	if (Val >= 100 && Val < 200) {
		ProPrice = 1;					//100�~���i�{�^��
	}
	else {
		ProPrice = 2;					//150�~���i�{�^��
	}

	/*�w���\������*/
	//100�~���i��I���̏ꍇ
	if (ProPrice == 1) {
		//���i�w�����\�ȏꍇ
		if (Totalprice >= UpPrice) {
			Judge = 1;
		}
		else {
			Judge = 0;
		}
	}
	//150�~�̏��i���I�����ꂽ�ꍇ
	else {
		//150�~�̏��i���w���\�ȏꍇ
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
//��邱��
//�E�G���[�A��O�����̒ǉ�
//�E�F�X���ߑł����č���Ă�Ƃ���̏C��
//---------------

/*-- �󂯎����������ɌŒ�l���ĕԂ� --*/
int main(void)
{
	//���[�v�J�n
	while (1)
	{

		bool Debugmode = true;		//�f�o�b�O���[�h�I���I�t
		char input[16];				//GUI����̓��͒l
		char Start[8];				//�J�n�ҋ@�p
		int EvID;					//�C�x���gID
		int EvValue;				//�C�x���g�l
		int UpBtn;					//��i�{�^��
		int DownBtn;				//���i�{�^��
		int Judge = 3;				//���i�w���\�����f
		int ProductU = 1;			//100�~���i�w���L��
		int ProductD = 1;			//150�~���i�w���L��
		int Change = 0;				//������z�@���[�v���n�܂�i�K��0�~�ɂ���

		//�f�o�b�O�p�R�����g
		if (Debugmode == false) printf("q�ȊO�ŃX�^�[�g�F");

		//�A�N�V����������܂őҋ@�p & �I��
		scanf_s("%s", Start, 6);

		if (Start[0] == 'q') {
			break;
		}

		/* �t�@�C���I�[�v�� */
		FILE* fp;
		if ((fp = fopen("GUI_File.txt", "r")) == NULL) {
			printf("file open error!!\n");
			exit(EXIT_FAILURE);		//�t�@�C�����ǂݍ��߂Ȃ���ΏI��				
		}


		/* �t�@�C���ǂݍ��� */
		(fgets(input, 7, fp));		//input�ɕ�����擾

		//�f�o�b�O�p�R�����g
		if(Debugmode == false)	printf("%s\n", input);	//������m�F�p


		//GUI����̃t�@�C���N���[�Y
		fclose(fp);


		/* �C�x���gID�ƃC�x���g�l�̎擾 */
		EvID = atoi(strtok(input, ","));			//�C�x���gID��int�^�Ŏ擾
		//�f�o�b�O�p�R�����g
		if (Debugmode == false)	printf("�C�x���gID�F%d\n", EvID);			

		EvValue = atoi(strtok(NULL, ","));			//�C�x���g�l��int�^�Ŏ擾
		//�f�o�b�O�p�R�����g
		if (Debugmode == false) printf("�C�x���g�l�F%d\n", EvValue);


		/* �C�x���gID�̔��� */
		switch (EvID) {
		//�C�x���gID = 1(���z����)�̏ꍇ
		case 1:
			EvValue = Total(EvValue);
			break;

		//�C�x���gID = 2(���i�{�^������)�̏ꍇ
		case 2:
			Judge = Product(EvValue);
			break;


		//�C�x���gID = 3(���背�o�[�������ꂽ)�̏ꍇ
		case 3:
			Change = Totalprice;		//������z��ۑ�
			Totalprice = 0;			//���v���z��0��
			EvValue = 0;			//�C�x���g�l�����Z�b�g
			break;


		//�C�x���gID�ΏۊO
		default:
			break;
		}

		/* ���i�w�����p�̏��� */
		if (Judge == 0) {
			/* ��i�E���i�{�^���̔��� */
			UpBtn = UpButton(Totalprice);
			DownBtn = DownButton(Totalprice);
		}
		//100�~�̏��i�w�����p�����[�^�ύX
		else if (Judge == 1) {
			UpBtn = 0;
			DownBtn = 0;
			ProductU = 2;
			Change = Totalprice - 100;
			Totalprice = 0;
			EvValue = 0;
		}
		//150�~�̏��i�w�����p�����[�^�ύX
		else if(Judge == 2){
			UpBtn = 0;
			DownBtn = 0;
			ProductD = 2;
			Change = Totalprice - 150;
			Totalprice = 0;
			EvValue = 0;
		}
		//�C�x���gID�Q�ȊO�̏���
		else {
			/* ��i�E���i�{�^���̔��� */
			UpBtn = UpButton(Totalprice);
			DownBtn = DownButton(Totalprice);
		}


		/*Debugmode�ŕ\�����e�̕ύX*/
		if (Debugmode == true) {
			/* GUI�ɕԂ��l�̕\�� */
			printf("%04d,%04d,%04d,%02d,%02d,%04d\n", UpBtn, DownBtn, Totalprice,ProductU,ProductD,Change);

			/* GUI�ɑ���t�@�C���̏o�� */
			fp = fopen("System_File.txt", "w");
			fprintf(fp, "%04d,%04d,%04d,%02d,%02d,%04d", UpBtn, DownBtn, Totalprice, ProductU, ProductD, Change);
			fclose(fp);
		}
		else {

			/* GUI�ɕԂ��l�̕\�� */
			printf("�o�͒l�F%04d,%04d,%04d,%02d,%02d,%04d\n", UpBtn, DownBtn, Totalprice,ProductU,ProductD,Change);

			/* GUI�ɑ���t�@�C���̏o�� */
			fp = fopen("System_File.txt", "w");
			fprintf(fp, "%04d,%04d,%04d,%02d,%02d,%04d", UpBtn, DownBtn, Totalprice, ProductU, ProductD, Change);
			fclose(fp);
		}
	}
	return 0;
}
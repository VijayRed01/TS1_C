/*
=======================================================================================================================
INFO
=======================================================================================================================
Title       : LC4_Comm_Lib  - Function that are writen for LC4
Author		: Slobodan Milosevic
Revision	: 1.0.0.1
Date		: 02.11.2015.
=======================================================================================================================
CHANGE LOG
=======================================================================================================================
1.0.0.0		: Initial Release
1.0.0.1		: Brasil update2
=======================================================================================================================
*/
#pragma once


/// Added to enforce __cdecl calling convetion
#define CALL __cdecl

#ifdef LC4_COMM_LIB_EXPORTS
#define COMMLIB_LC4_API __declspec(dllexport)
#else
#define COMMLIB_LC4_API __declspec(dllimport)
#endif


#ifdef __cplusplus
extern "C" {
#endif


	typedef struct LC4SystemInformation {
		char CalibrationDate[64];
		char Calibrationvalidity;
		char BSMInumber[64];
		char LC4_fw[64];
		char PL4_fw[64];
		char PL4_checksum[64];
	}LC4SystemInformation, *ptrLC4SystemInformation;

	/* open usb communication */
	long COMMLIB_LC4_API Open_USB_Comm();
	/* open wifi communication */
	long COMMLIB_LC4_API Open_WiFi_Comm(const char* ipAddress);
	/* set encryption key */
	long COMMLIB_LC4_API Set_Encryption_Key(const char* key);
	/* decrypt encrypted file */
	long COMMLIB_LC4_API Decrypt_File(const char* encryptedFilePath, const char* _decryptedFilePath);
	/* create list of files */
	long COMMLIB_LC4_API Create_Available_File_List(int type, const char* time, const char* listPath);
	/* get file from device */
	long COMMLIB_LC4_API Get_File(const char* Lc4_Path, const char* pc_Path, bool encFlag);
	/* close communication whith LC4 device */
	void COMMLIB_LC4_API Close_Comm();
	/* power off LC4 device */
	long COMMLIB_LC4_API Power_Down();
	/* get system information */
	long COMMLIB_LC4_API Get_SysInfo(ptrLC4SystemInformation sysInfo);
	/* set settings path */
	void COMMLIB_LC4_API Set_Settings_Path(const char* errorFilePath);
	/* get error code*/
	long COMMLIB_LC4_API Get_Error_Code();
	/* get error string*/
	size_t COMMLIB_LC4_API Get_Error_String(char* errStr);
	/* get current file progres - for downloaded file */
	size_t COMMLIB_LC4_API Get_File_Progress();
	/* get size of file */
	size_t COMMLIB_LC4_API Get_File_Size();
	/* Serial Number */
	int COMMLIB_LC4_API cmd_GetLidarSerialNo(char *);
#ifdef __cplusplus
}
#endif

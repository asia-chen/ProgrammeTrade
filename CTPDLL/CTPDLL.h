#pragma once

#include "include\ThostFtdcMdApi.h"
#include "include\ThostFtdcTraderApi.h"
#include "include\ThostFtdcUserApiDataType.h"
#include "include\ThostFtdcUserApiStruct.h"

#include "tools.h"

typedef void (*CallbackDelegate)(const char* inputStr, int nRequestID, int bIsLast);
#define DLLEXPORT extern "C" __declspec(dllexport)

DLLEXPORT int TradeInitAPI(char *server_addr, CallbackDelegate callback);
DLLEXPORT int TradeSendRequest(char *req);
static CallbackDelegate tradeCallback = NULL;
int TradeResponse(char *result,int nRequestID, int bIsLast);
int MdResponse(char *result);



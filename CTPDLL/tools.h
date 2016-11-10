#include <string>
#include <vector>
#include <time.h>

using namespace std;

#define tempstrLen 50


int split(char *instr, vector<string> *v);
string ltos(long long lInput);
string itos(int lInput);
long long dtol(double input);
string gettime();
string dtos(double dInput);
string ctos(char c);

#define DOUBLEMAX 9999999999999
#define DOUBLEMIN -9999999999999

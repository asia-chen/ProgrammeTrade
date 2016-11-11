#include <string>
#include <vector>
#include <time.h>

using namespace std;

#define tempstrLen 500

int split(char *instr, vector<string> *v);
string ltos(long long lInput);
long long dtol(double input);
string gettime();
string ctos(char c);
string dtos(double dInput);

#define DOUBLEMAX 9999999999999
#define DOUBLEMIN -9999999999999
#include <stddef.h>
#include <stdio.h>

struct stc 
{
  char c;
  int l;
  int i;
  long long ll;
  float f;
  double d;
  char arr[12];
  char end;
};

int main() 
{
  printf("offset\nc: %lu, l: %lu, i: %lu, ll: %lu, f: %lu, d: %lu, arr: %lu, end: %lu\nsizeof: %lu",
         offsetof(struct stc, c), offsetof(struct stc, l),
         offsetof(struct stc, i), offsetof(struct stc, ll),
         offsetof(struct stc, f), offsetof(struct stc, d),
         offsetof(struct stc, arr), offsetof(struct stc, end),
        sizeof(struct stc));
  return 0;
}

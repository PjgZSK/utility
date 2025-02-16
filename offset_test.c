#include <stddef.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

struct stc 
{
  char c;
  short si;
  int i;
  long long ll;
  float f;
  double d;
  char arr[12];
  char end;
};

struct cache_time {
	unsigned int sec;
	unsigned int nsec;
};
struct cache_entry {
	struct cache_time ctime;
	struct cache_time mtime;
	unsigned int st_dev;
	unsigned int st_ino;
	unsigned int st_mode;
	unsigned int st_uid;
	unsigned int st_gid;
	unsigned int st_size;
	unsigned char sha1[20];
	unsigned short namelen;
	unsigned char name[0];
};
#define cache_entry_size(len) ((offsetof(struct cache_entry,name) + (len) + 8) & ~7)
#define ce_size(ce) cache_entry_size((ce)->namelen)

int main() 
{
  printf("stc offset\nc: %lu, si: %lu, i: %lu, ll: %lu, f: %lu, d: %lu, arr: %lu, end: %lu\nsizeof: %lu\n",
         offsetof(struct stc, c), offsetof(struct stc, si),
         offsetof(struct stc, i), offsetof(struct stc, ll),
         offsetof(struct stc, f), offsetof(struct stc, d),
         offsetof(struct stc, arr), offsetof(struct stc, end),
        sizeof(struct stc));
  
  struct cache_entry* ce = malloc(sizeof(struct cache_entry));
  ce->namelen = 4;
  memcpy(ce->name, "file", ce->namelen);
  printf("cache_entry\nce_size: %lu, sizeof: %lu, name offset: %lu", ce_size(ce), sizeof(struct cache_entry), offsetof(struct cache_entry, name));
  return 0;
}

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main()
{
    char* str = (char*)malloc(sizeof(char)*4);
    // big-endian 'UN'
    short* p1 = (short*)malloc(sizeof(short));
    *p1 = 85 * (1 << 8) + 78; 
    char* p2 = (char*)p1;
    sprintf(str, "%c%c", *p2, *(p2 + 1));

    // big-endian 'IX'
    short* p3 = (short*)malloc(sizeof(short));
    *p3 = 73 * (1 << 8) + 88; 
    char* p4 = (char*)p3;
    sprintf(str + 2, "%c%c", *p4, *(p4 + 1));

    if (0 == strcmp("UNIX", str))
        printf("big-endianness,");
    else
        printf("little-endianness,");
    printf("%s", str);

    return 0;
}

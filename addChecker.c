#include <stdio.h>

int main()
{
    unsigned a = ~(1 << 31) + (1 << 31);
    printf("%u, %u", a, a + 1);
    return 0;
}

def rol(byte, n): # assumes that n is already modulo'd by 8
    return ((byte << n) & 0xff) | (byte >> (8-n))

def solve_cheshire():
    key = "41!ce1337" # key obtained from code
    output = "oI!&}IusoKs ?Ytr"[::-1] # reverse the output string
    inp = "" # input string

    for i, char in enumerate(output):
        inp += chr(ord(char) ^ ord(key[i % len(key)])) # perform xor

    print(inp[::-1]) # reverse the input string and print it to console

def solve_caterpillar():
    key = "c4t3rp1114rz_s3cr3t1y_ru13_7h3_w0r1d"
    output = "\0R\u009c\u007f\u0016ndC\u0005î\u0093MíÃ×\u007f\u0093\u0090\u007fS}­\u0093)ÿÃ\f0\u0093g/\u0003\u0093+Ã¶\0Rt\u007f\u0016\u0087dC\aî\u0093píÃ8\u007f\u0093\u0093\u007fSz­\u0093ÇÿÃÓ0\u0093\u0086/\u0003q"[::-1] # reverse output string
    inp = ""

    for i, char in enumerate(output):
        b = (rol(ord(char), 2) + 127) % 256 # reverse of b = this.rol(b - 127, 6);
        b ^= ord(key[i % len(key)]) # reverse of b ^= Convert.ToByte(text[i % text.Length]);
        b = rol((b - 222) % 256, 6) # reverse of this.rol(b, 114) + 222;
        inp += chr(b)

    print(inp[::-1])

if __name__ == '__main__':
    solve_cheshire()
    solve_caterpillar()
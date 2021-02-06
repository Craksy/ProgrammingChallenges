# 04 - Encryption algorithm

Encrypts or decrypts a file using a provided key.

This is an extremely simplified take on an SP-network. 

### Algorithm
The algorithm works by first computing the MD5 hash of the provided key, in order to expand it to 128bits,
and then going through 16 rounds of:
1. split every byte in 2 halfs
2. translate each of those in a 4bit substitution table
3. rejoin them to get a byte
4. permutate the bits of that byte
5. XOR this byte with one byte of the expanded key
6. The resulting byte becomes the input for the next round

Decryption is done by going through the same process in reverse.

The subtitution and permutation tables were pretty much chosen randomly.

###### Example output
**CLI help message**
```
EncryptionAlgorithm 1.0.0
Copyright (C) 2021 EncryptionAlgorithm

  -e, --encrypt    (Group: mode)

  -d, --decrypt    (Group: mode)

  -k, --key        Required.

  -o, --out        Output file. If not specified output to stdout

  --help           Display this help screen.

  --version        Display version information.

  value pos. 0     Required. The input file to operate on.
```

### Thoughts
When I first started looking into encryption algorithms I had no idea what i was getting myself into. It's a rabbit hole i could easily get lost in for days.

There are several things that I don't quite understand yet, and I have probably done some things wrong.
For instance the difference between the SBox and PBox is a bit unclear to me. From the resources I've read the both seem to be doing permutation.

I guess that I might have oversimplified my implementation a bit too much. I could propably also have benefited from studying other implementations before diving head first into it.

It's also my first time making a proper CLI in C#. Honestly I just wanted to be done in the end, but i probably could've given it some more love.


// When using IL2CPP, Unity still tries to link with the static libopusfm library even if the shared library will be used.
// This file creates dummy functions just to please the linker.

void OpusFMEncoderCreate() {}
void OpusFMEncoderDestroy() {}
void OpusFMEncoderEncode() {}
void OpusFMEncoderSetConfig() {}
void OpusFMEncoderSetBitrate() {}
void OpusFMDecoderCreate() {}
void OpusFMDecoderDestroy() {}
void OpusFMDecoderDecode() {}
void OpusFMDecoderDecodeFec() {}

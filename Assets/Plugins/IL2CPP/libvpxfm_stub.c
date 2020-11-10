
// When using IL2CPP, Unity still tries to link with the static libvpxfm library even if the shared library will be used.
// This file creates dummy functions just to please the linker.

void VpxFMEncoderCreate() {}
void VpxFMEncoderDestroy() {}
void VpxFMEncoderEncodei420() {}
void VpxFMEncoderGetForceKeyFrame() {}
void VpxFMEncoderSetForceKeyFrame() {}
void VpxFMEncoderGetBitrate() {}
void VpxFMEncoderSetBitrate() {}
void VpxFMEncoderSetVP9() {}
void VpxFMEncoderSetConfig() {}
void VpxFMDecoderCreate() {}
void VpxFMDecoderDestroy() {}
void VpxFMDecoderDecode() {}
void VpxFMDecoderGetNeedsKeyFrame() {}
void VpxFMDecoderSetNeedsKeyFrame() {}
void VpxFMDecoderSetVP9() {}

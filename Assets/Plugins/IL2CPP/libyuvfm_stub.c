
// When using IL2CPP, Unity still tries to link with the static libyuvfm library even if the shared library will be used.
// This file creates dummy functions just to please the linker.

void YuvFMCreate() {}
void YuvFMDestroy() {}
void YuvFMConvertToi420() {}
void YuvFMConvertFromI420() {}
void YuvFMI420Rotate() {}
void YuvFMScalei420() {}

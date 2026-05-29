#!/bin/bash
set -e

PLATFORM="linux-x64"
LIB_NAME="libluau.so"
JOBS=$(nproc)

# Build
rm -rf build
mkdir build && cd build
cmake .. -DCMAKE_BUILD_TYPE=Release
cmake --build . --config Release -- -j$JOBS

# Copy to native
mkdir -p ../src/LuauInterop.Native/runtimes/$PLATFORM/native
cp $LIB_NAME ../src/LuauInterop.Native/runtimes/$PLATFORM/native/$LIB_NAME

echo "Done! $LIB_NAME copied to src/LuauInterop.Native/runtimes/$PLATFORM/native/"

# Cleanup
cd ..
rm -rf build
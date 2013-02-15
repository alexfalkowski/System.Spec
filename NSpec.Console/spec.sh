#!/usr/bin/env bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

exec mono --runtime=v4.0 $DIR/spec.exe "$@"
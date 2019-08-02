package com.thinktecture.boardist.core;

import java.io.FileInputStream;

public class FileLoadResult {

    private FileInputStream stream;
    private String mimeType;

    public FileLoadResult(FileInputStream stream, String mimeType) {
        this.mimeType = mimeType;
        this.stream = stream;
    } 

    public FileInputStream getStream() {
        return this.stream;
    }

    public String getMimeType() {
        return this.mimeType;
    }
}
namespace ToDo
{
    public static class Platform
    {
        public static bool IsWasm =
#if __WASM__
            true;

#else
            false;
#endif
    }
}

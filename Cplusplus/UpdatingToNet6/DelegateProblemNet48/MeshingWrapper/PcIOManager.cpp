#include "PcIOManager.h"
#include <stdexcept>
#include <msclr/marshal_cppstd.h>

#define ERROR_CODE "M72P0007"

namespace MeshingWrapper {

	bool PcIOManager::Load(System::String^ path, IProgressObserver^ progressObserver)
	{
		try {

			msclr::interop::marshal_context context;

			ProgressObserverWrapper wrapper(progressObserver);
			std::string standardString = context.marshal_as<std::string>(path);
			return true;
		}
		catch (const std::exception& e) {
			throw gcnew System::Exception(gcnew System::String(e.what()));
		}
	}

}
#undef ERROR_CODE
#include "ProgressObserverWrapper.h"
using namespace System::Runtime::InteropServices;
namespace MeshingWrapper 
{
	NativeProgressObserver::NativeProgressObserver(CancelledFun c, NotifyFun f):
		m_canceled(c),
		m_notify(f)
	{
	}

	bool NativeProgressObserver::canceled() const
	{
		if (m_canceled != nullptr) {
			return(*m_canceled)();
		}
		return false;
	}

	void NativeProgressObserver::notifyProgress(double percentage)
	{
		if (m_notify) {
			(*m_notify)(percentage);
		}
	}

	ProgressObserverWrapper::ProgressObserverWrapper(IProgressObserver^ prg):
		m_progressObserver(prg)
	{
		if (m_progressObserver)
		{
			m_cancelDelegate = gcnew CancelledDelegate(m_progressObserver, &IProgressObserver::Canceled);
			m_notifyDelegate = gcnew NotifyDelegate(m_progressObserver, &IProgressObserver::Notify);
			auto cancPtr = static_cast<NativeProgressObserver::CancelledFun>(Marshal::GetFunctionPointerForDelegate(m_cancelDelegate).ToPointer());
			auto notifyPtr = static_cast<NativeProgressObserver::NotifyFun>(Marshal::GetFunctionPointerForDelegate(m_notifyDelegate).ToPointer());
			m_nativeProgress = new NativeProgressObserver(cancPtr, notifyPtr);
		}
	}

	ProgressObserverWrapper::~ProgressObserverWrapper()
	{
		this->!ProgressObserverWrapper();
	}

	ProgressObserverWrapper::!ProgressObserverWrapper()
	{
		if (m_nativeProgress) {
			delete m_nativeProgress;
			m_nativeProgress = nullptr;
		}
	}

	NativeProgressObserver* ProgressObserverWrapper::nativeProgress()
	{
		return m_nativeProgress;
	}
}
